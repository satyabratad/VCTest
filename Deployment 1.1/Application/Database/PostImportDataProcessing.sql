IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PostImportDataProcessing')
DROP PROCEDURE PostImportDataProcessing
GO

-- =============================================
-- Author:		RS Software
-- Create date: 03/10/2017
-- Description:	Executing Data processing after transaction imported into Staging
-- =============================================
CREATE PROCEDURE [PostImportDataProcessing]
	@YEAR INT = 2016,
	@USERID BIGINT=1,
	@TOTALCOUNT INT=0,
	@FILENAME VARCHAR(255),
	@PAYERID INT
AS
	DECLARE 
	@SUMMARYID INT,
	@PROCESSLOG NVARCHAR(1024)='',
	@RECORDCOUNT INT;

	INSERT INTO ImportSummaries(PaymentYear,ImportDate,UserId,DateAdded,IsActive)
	values (@YEAR,GETDATE(),@USERID,GETDATE(),1)
	SET @SUMMARYID = @@IDENTITY

	DECLARE @K1099SUMMARYCHART TABLE(
		TransactionYear INT,
		PayeeAccountNumber NVARCHAR(20),
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

	SET @PROCESSLOG = '1099K: Import Process Starts' +CHAR(13)+CHAR(10)
	SET @PROCESSLOG = @PROCESSLOG + 'Import Date: '+CAST(GETDATE() AS VARCHAR) +CHAR(13)+CHAR(10)
	SET @PROCESSLOG = @PROCESSLOG + 'File Name: '+@FILENAME+CHAR(13)+CHAR(10)
	SET @PROCESSLOG = @PROCESSLOG + 'Transaction Count: '+CAST(@TOTALCOUNT AS VARCHAR) +CHAR(13)+CHAR(10)
	
	-- ARCHIVE EXISTING DATA
	UPDATE [RawTransactions] SET Isactive = 0 WHERE IsActive=1
	
	BEGIN TRY

		-- INSERT NEW DATA
		INSERT INTO [RawTransactions](PayeeAccountNumber,TransactionType,TransactionAmount,TransactionDate,IsActive,UserID,DateAdded)
		SELECT 
		[PayeeAccountNumber], 
		CASE WHEN [TransactionType] = 7 THEN 'CNP' ELSE 'CP' END AS [TransactionType],
		CAST([TransactionAmount] AS DECIMAL(19,2)) AS TransactionAmount, 
		CAST([TransactionDate] AS DATE) AS TransactionDate,
		1,
		@USERID,
		GETDATE()
		FROM [RawTransactionStagings]

		SET @RECORDCOUNT = @@ROWCOUNT
		
	END TRY
	BEGIN CATCH
	PRINT ERROR_MESSAGE();
	SET @PROCESSLOG = @PROCESSLOG + 'INVALID DATA : '+ERROR_MESSAGE()+' ,ERROR CODE : ' +CAST(ERROR_NUMBER() AS VARCHAR)+''+CHAR(13)+CHAR(10)
	ROLLBACK TRANSACTION K1099
	GOTO ENDPROCESS
	END CATCH;
	
	INSERT INTO @K1099SUMMARYCHART
	SELECT * FROM [ImportDataSummary](@PAYERID,@YEAR)

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
	SELECT @PAYERNAME = p.FirstPayerName FROM [PayerDetails] p where Id = @PAYERID

	INSERT INTO ImportDetails (AccountNumber,ImportSummaryId,TINCheckStatus,TINCheckRemarks,SubmissionSummaryId,TINType,TIN,
	PayerOfficeCode,GrossAmount,CNPTransactionAmount,FederalWithHoldingAmount,
	JanuaryAmount,FebruaryAmount,MarchAmount,AprilAmount,MayAmount,JuneAmount,JulyAmount,AugustAmount,
	SeptemberAmount,OctoberAmount,NovemberAmount,DecemberAmount,ForeignCountryIndicator,FirstPayeeName,
	SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZipCode,SecondTINNoticed,FillerIndicatorType,
	PaymentIndicatorType,TransactionCount,MerchantId,MerchantCategoryCode,SpecialDataEntry,StateWithHolding,
	LocalWithHolding,CFSF,IsActive,DateAdded)

	SELECT C.PayeeAccountNumber,@SUMMARYID,C.TINCheckStatus,C.TINCheckRemarks,C.SubmissionSummaryId,D.TINType,D.PayeeTIN,
	D.PayeeOfficeCode,C.GrossAmount,C.TotalCPAmount,NULL,
	C.JANUARY,C.FEBRUARY,C.MARCH,C.APRIL,C.MAY,C.JUNE,C.JULY,C.AUGUST,
	C.SEPTEMBER,C.OCTOBER,C.NOVEMBER,C.DECEMBER,NULL,SUBSTRING(D.[FirstPayeeName],1,40), 
	SUBSTRING(D.[SecondPayeeName],1,40),SUBSTRING(D.[PayeeMailingAddress],1,40),SUBSTRING(D.[PayeeCity],1,40),D.[PayeeState],REPLACE(D.[PayeeZIP],'-',''),null,D.[FilerIndicatorType], 
	D.[PaymentIndicatorType],C.TotalTransaction,D.Id,D.[MCC],NULL,NULL,
	NULL,D.CFSF,1,GETDATE()

	FROM @K1099SUMMARYCHART C
	INNER JOIN  [MerchantDetails] D ON C.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PayerId = @PAYERID
	WHERE ISNULL(C.StatusId,0) IN (0,1,2,3,4)

	SET @PROCESSLOG = @PROCESSLOG + 'Account associated with '+@PAYERNAME+':'+CAST(@@ROWCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)

	----- Correction ------
	INSERT INTO SubmissionStatus (PaymentYear,AccountNumber,ProcessingDate,StatusId,IsActive,DateAdded)
	SELECT @YEAR ,C.PayeeAccountNumber ,GetDate(),4,1,GetDate()
	FROM  @K1099SUMMARYCHART C
	WHERE C.StatusId=3
	--------------------------------------------------------------------

	

	DECLARE @ORPHANT NVARCHAR(1024),@ORPHANTCOUNT INT

	SELECT @ORPHANTCOUNT = COUNT(*)
	FROM @K1099SUMMARYCHART S
	LEFT JOIN  [MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PayerId = @PayerId
	WHERE S.TransactionYear = @YEAR AND ISNULL(S.StatusId,0) NOT IN (0,1,2,3,4)


	IF @ORPHANTCOUNT>0
	BEGIN
		SET @PROCESSLOG = @PROCESSLOG + 'Account not associated with '+@PAYERNAME+':'+CAST(@ORPHANTCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)
	END

	SET @PROCESSLOG = @PROCESSLOG + 'Import Successful'+CHAR(13)+CHAR(10)

		COMMIT TRANSACTION K1099
		END TRY  
	BEGIN CATCH  
		SET @PROCESSLOG = @PROCESSLOG + 'EXCEPTION : '+ERROR_MESSAGE()
		ROLLBACK TRANSACTION K1099
	END CATCH 
	
	ENDPROCESS:

	UPDATE ImportSummaries SET
		RecordCount = @RECORDCOUNT,
		ProcessLog = @PROCESSLOG,
		[FileName] = @FILENAME,
		ImportDate = GETDATE()
	WHERE Id = @SUMMARYID
	
	

RETURN
