IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PostImportDataProcessing')
DROP PROCEDURE PostImportDataProcessing
GO
CREATE PROCEDURE [dbo].[PostImportDataProcessing]
	@YEAR INT = 2016,
	@UserId BIGINT=1,
	@TotalCount INT=0,
	@FileName VARCHAR(255),
	@PayerId int
AS
	DECLARE 
	@SummaryId INT,
	@ProcessLog NVARCHAR(1024)='',
	@RecordCount INT;

	INSERT INTO ImportSummaries(PaymentYear,ImportDate,UserId,DateAdded,IsActive)
	values (@YEAR,GETDATE(),@UserId,GETDATE(),1)
	SET @SummaryId = @@IDENTITY

	DECLARE @K1099SUMMARYCHART TABLE(
		TransactionYear INT,
		PayeeAccountNumber VARCHAR(255),
		JANUARY DECIMAL(19,2),
		FEBRUARY DECIMAL(19,2),
		MARCH DECIMAL(19,2),
		APRIL DECIMAL(19,2),
		MAY DECIMAL(19,2),
		JUNE DECIMAL(19,2),
		JULY DECIMAL(19,2),
		AUGUST DECIMAL(19,2),
		SEPTEMBER DECIMAL(19,2),
		OCTOBER DECIMAL(19,2),
		NOVEMBER DECIMAL(19,2),
		DECEMBER DECIMAL(19,2),
		TotalCPAmount DECIMAL(19,2),
		GrossAmount DECIMAL(19,2),
		TotalTransaction INT,
		ImportDetailsId INT,
		SubmissionSummaryId INT,
		SubmissionStatusId INT,
		TINCheckStatus NVARCHAR(2),
		TINCheckRemarks NVARCHAR(512),
		StatusId INT
	)

	BEGIN TRY  
	BEGIN TRANSACTION K1099

	SET @ProcessLog = '1099K: Import Process Starts' +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Import Date: '+CAST(GETDATE() AS VARCHAR) +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'File Name: '+@FileName+CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Transaction Count: '+CAST(@TotalCount AS VARCHAR) +CHAR(13)+CHAR(10)
	
	-- ARCHIVE EXISTING DATA
	UPDATE [dbo].[RawTransactions] SET Isactive = 0 WHERE IsActive=1
	
	BEGIN TRY

		-- INSERT NEW DATA
		INSERT INTO [dbo].[RawTransactions](PayeeAccountNumber,TransactionType,TransactionAmount,TransactionDate,IsActive,UserID,DateAdded)
		SELECT 
		[PayeeAccountNumber], 
		CASE WHEN [TransactionType] = 7 THEN 'CNP' ELSE 'CP' END AS [TransactionType],
		CAST([TransactionAmount] AS DECIMAL(19,2)) AS TransactionAmount, 
		CAST([TransactionDate] AS DATE) AS TransactionDate,
		1,
		@UserId,
		GETDATE()
		FROM [dbo].[RawTransactionStagings]

		SET @RecordCount = @@ROWCOUNT
		
	END TRY
	BEGIN CATCH
	PRINT ERROR_MESSAGE();
	SET @ProcessLog = @ProcessLog + 'INVALID DATA : '+ERROR_MESSAGE()+' ,ERROR CODE : ' +CAST(ERROR_NUMBER() AS VARCHAR)+''+CHAR(13)+CHAR(10)
	ROLLBACK TRANSACTION K1099
	GOTO ENDPROCESS
	END CATCH;
	
	INSERT INTO @K1099SUMMARYCHART
	SELECT * FROM [dbo].[ImportDataSummary](@PayerId,@YEAR)

	-- ARCHIVE EXISTING SUBMISSION STATUS
	UPDATE S
	SET S.IsActive = 0
	FROM SubmissionStatus S
	INNER JOIN @K1099SUMMARYCHART C ON S.Id = C.SubmissionStatusId AND ISNULL(C.StatusId,0) IN (0,1,2,3,4)
	
	-- CLEAR EXISTING DATA THAT ARE NOT SUBMITTED
	UPDATE D
	SET D.IsActive = 0
	FROM ImportDetails D
	INNER JOIN @K1099SUMMARYCHART C ON D.Id = C.ImportDetailsId AND ISNULL(C.StatusId,0) IN (0,1,2,3,4)

	DECLARE @PAYERNAME VARCHAR(127)
	SELECT @PAYERNAME = p.FirstPayerName FROM [dbo].[PayerDetails] p where Id = @PayerId

	INSERT INTO ImportDetails (AccountNo,ImportSummaryId,TINCheckStatus,TINCheckRemarks,SubmissionSummaryId,TINType,TIN,
	PayerOfficeCode,GrossAmount,CNPTransactionAmount,FederalWithHoldingAmount,
	JanuaryAmount,FebruaryAmount,MarchAmount,AprilAmount,MayAmount,JuneAmount,JulyAmount,AugustAmount,
	SeptemberAmount,OctoberAmount,NovemberAmount,DecemberAmount,ForeignCountryIndicator,FirstPayeeName,
	SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZipCode,SecondTINNoticed,FillerIndicatorType,
	PaymentIndicatorType,TransactionCount,MerchantId,MerchantCategoryCode,SpecialDataEntry,StateWithHolding,
	LocalWithHolding,CFSF,IsActive,DateAdded)

	SELECT C.PayeeAccountNumber,@SummaryId,C.TINCheckStatus,C.TINCheckRemarks,C.SubmissionSummaryId,D.TINType,D.PayeeTIN,
	D.PayeeOfficeCode,C.GrossAmount,C.TotalCPAmount,NULL,
	C.JANUARY,C.FEBRUARY,C.MARCH,C.APRIL,C.MAY,C.JUNE,C.JULY,C.AUGUST,
	C.SEPTEMBER,C.OCTOBER,C.NOVEMBER,C.DECEMBER,NULL,SUBSTRING(D.[PayeeFirstName],1,40), 
	SUBSTRING(D.[PayeeSecondName],1,40),SUBSTRING(D.[PayeeMailingAddress],1,40),SUBSTRING(D.[PayeeCity],1,40),D.[PayeeState],REPLACE(D.[PayeeZIP],'-',''),null,D.[FilerIndicatorType], 
	D.[PaymentIndicatorType],C.TotalTransaction,D.Id,D.[MCC],NULL,NULL,
	NULL,D.CFSF,1,GETDATE()

	FROM @K1099SUMMARYCHART C
	INNER JOIN  [dbo].[MerchantDetails] D ON C.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PayerId = @PayerId
	WHERE ISNULL(C.StatusId,0) IN (0,1,2,3,4)

	SET @ProcessLog = @ProcessLog + 'Account associated with '+@PAYERNAME+':'+CAST(@@ROWCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)

	----- Correction ------
	INSERT INTO SubmissionStatus (PaymentYear,AccountNumber,ProcessingDate,StatusId,IsActive,DateAdded)
	SELECT @YEAR ,C.PayeeAccountNumber ,GetDate(),4,1,GetDate()
	FROM  @K1099SUMMARYCHART C
	WHERE C.StatusId=3
	--------------------------------------------------------------------

	

	DECLARE @ORPHANT NVARCHAR(1024),@ORPHANTCOUNT INT

	SELECT @ORPHANTCOUNT = COUNT(*)
	FROM @K1099SUMMARYCHART S
	LEFT JOIN  [dbo].[MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PayerId = @PayerId
	WHERE S.TransactionYear = @YEAR AND ISNULL(S.StatusId,0) NOT IN (0,1,2,3,4)


	IF @ORPHANTCOUNT>0
	BEGIN
		SET @ProcessLog = @ProcessLog + 'Account not associated with '+@PAYERNAME+':'+CAST(@ORPHANTCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)
	END

	SET @ProcessLog = @ProcessLog + 'Import Successful'+CHAR(13)+CHAR(10)

		COMMIT TRANSACTION K1099
		END TRY  
	BEGIN CATCH  
		SET @ProcessLog = @ProcessLog + 'EXCEPTION : '+ERROR_MESSAGE()
		ROLLBACK TRANSACTION K1099
	END CATCH 
	
	ENDPROCESS:

	UPDATE ImportSummaries SET
		RecordCount = @RecordCount,
		ProcessLog = @ProcessLog,
		[FileName] = @FileName,
		ImportDate = GETDATE()
	WHERE Id = @SummaryId
	
	

RETURN
