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
		OCTOBOR DECIMAL(19,2),
		NOVEMBER DECIMAL(19,2),
		DECEMBER DECIMAL(19,2),
		TotalCPAmount DECIMAL(19,2),
		GrossAmount DECIMAL(19,2),
		TotalTransaction INT
	)

	IF OBJECT_ID('tempdb..#TEMP_SUMMARY') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY
	END
	
	
	IF OBJECT_ID('tempdb..#SUBMITTED') IS NOT NULL
	BEGIN
	DROP TABLE #SUBMITTED
	END

	BEGIN TRY  
	BEGIN TRANSACTION K1099

	SET @ProcessLog = '1099K: Import Process Starts' +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Import Date: '+CAST(GETDATE() AS VARCHAR) +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'File Name: '+@FileName+CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Transaction Count: '+CAST(@TotalCount AS VARCHAR) +CHAR(13)+CHAR(10)

	SELECT D.Id, D.AccountNo,P.Id AS PayerId
	INTO #SUBMITTED
	FROM ImportDetails D
	INNER JOIN [dbo].[ImportSummaries] S ON S.Id = D.ImportSummaryId
	INNER JOIN [dbo].[MerchantDetails] M ON M.ID = D.MerchantId
	INNER JOIN [dbo].[PayerDetails] P ON P.ID = M.PayerId AND P.ID = @PayerId
	INNER JOIN [dbo].[SubmissionStatus] SS ON D.AccountNo = SS.AccountNumber AND S.PaymentYear = SS.PaymentYear
	WHERE D.IsActive =1  AND S.IsActive = 1 AND SS.IsActive =1 AND   SS.StatusId > 2 AND S.PaymentYear = @YEAR

	--FROM ImportDetails D
	--INNER JOIN [dbo].[ImportSummaries] S ON S.Id = D.ImportSummaryId
	--INNER JOIN [dbo].[MerchantDetails] M ON M.ID = D.MerchantId
	--INNER JOIN [dbo].[PayerDetails] P ON P.ID = M.PayerId AND P.ID = @PayerId
	--LEFT JOIN [dbo].[SubmissionStatus] SS ON D.AccountNo = SS.AccountNumber AND S.PaymentYear = SS.PaymentYear
	--WHERE D.IsActive =1 AND S.PaymentYear = @Year AND S.IsActive = 1 AND (D.SubmissionSummaryId IS NULL OR SS.StatusId <= 2)

	-- ARCHIVE EXISTING DATA
	UPDATE [dbo].[RawTransactions] SET Isactive = 0 WHERE IsActive=1
	--## ALTERNATIVE --DELETE FROM [dbo].[RawTransactions]
	
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
	
	-- GROUP BY MONTH,YEAR : MONTHLY SUMMARY
	SELECT TransactionYear,TransactionMonth,[PayeeAccountNumber],SUM(TransactionAmount) AS TransactionAmount,TransactionType,COUNT(1) AS TransactionCount
	INTO #TEMP_SUMMARY 
	FROM 
		(SELECT [PayeeAccountNumber],YEAR(TransactionDate) AS TransactionYear,
		MONTH(TransactionDate) AS TransactionMonth,TransactionAmount,TransactionType
		FROM [dbo].[RawTransactions] where IsActive = 1
	) P
	
	GROUP BY 
	TransactionYear,TransactionMonth,[PayeeAccountNumber],TransactionType
	ORDER BY 
	TransactionMonth,PayeeAccountNumber

	-- PIVOT
	INSERT INTO @K1099SUMMARYCHART(TransactionYear,PayeeAccountNumber
	,JANUARY,FEBRUARY,MARCH,APRIL,MAY,JUNE,JULY,AUGUST,SEPTEMBER,OCTOBOR,NOVEMBER,DECEMBER)
	SELECT TransactionYear,
	PayeeAccountNumber,
	[1] AS JANUARY,
	[2] AS FEBRUARY,
	[3] AS MARCH,
	[4] AS APRIL,
	[5] AS MAY,
	[6] AS JUNE,
	[7] AS JULY,
	[8] AS AUGUST,
	[9] AS SEPTEMBER,
	[10] AS OCTOBOR,
	[11] AS NOVEMBER,
	[12] AS DECEMBER
	--INTO #TEMP_SUMMARY_PIVOT
	FROM 
	(
	  SELECT TransactionYear,TransactionMonth,PayeeAccountNumber,TransactionAmount
	  FROM #TEMP_SUMMARY
	) src
	pivot
	(
	  SUM(TransactionAmount) 
	  FOR TransactionMonth in ([1], [2], [3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
	) PIV

	ORDER BY PAYEEACCOUNTNUMBER


	UPDATE CHART SET
		CHART.GrossAmount = GOSS.TransactionAmount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
		SELECT TransactionYear,PayeeAccountNumber,SUM(TransactionAmount) AS TransactionAmount
			FROM #TEMP_SUMMARY
			GROUP BY TransactionYear,PayeeAccountNumber
		)GOSS ON GOSS.TransactionYear= CHART.TransactionYear 
		AND GOSS.PayeeAccountNumber = CHART.PayeeAccountNumber 

	UPDATE CHART SET
		CHART.TotalCPAmount = CNP.TransactionAmount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
			SELECT TransactionYear,PayeeAccountNumber,TransactionType, SUM(TransactionAmount) AS TransactionAmount
			FROM #TEMP_SUMMARY
			GROUP BY TransactionYear,PayeeAccountNumber,TransactionType
		)CNP ON CNP.TransactionYear= CHART.TransactionYear 
		AND CNP.PayeeAccountNumber = CHART.PayeeAccountNumber AND CNP.TransactionType = 'CNP'
	
	UPDATE CHART SET
		CHART.TotalTransaction = C.TransactionCount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
		SELECT TransactionYear,PayeeAccountNumber,SUM(TransactionCount) AS TransactionCount
		FROM #TEMP_SUMMARY
		GROUP BY TransactionYear,PayeeAccountNumber
	)C ON C.TransactionYear= CHART.TransactionYear 
		AND C.PayeeAccountNumber = CHART.PayeeAccountNumber

	-- CLEAR EXISTING DATA THAT ARE NOT SUBMITTED
	UPDATE D
	SET D.IsActive = CASE WHEN SS.Id IS NULL THEN 0 ELSE D.IsActive END
	FROM ImportDetails D
	INNER JOIN [dbo].[MerchantDetails] M ON M.ID = D.MerchantId
	INNER JOIN [dbo].[PayerDetails] P ON P.ID = M.PayerId AND P.ID = @PayerId
	LEFT JOIN #SUBMITTED SS ON D.Id = SS.Id  
	WHERE D.IsActive =1 

	DELETE C 
	FROM @K1099SUMMARYCHART C
	INNER JOIN #SUBMITTED S ON C.PayeeAccountNumber=S.AccountNo 
	
	DECLARE @PAYERNAME VARCHAR(127)
	SELECT @PAYERNAME = p.FirstPayerName FROM [dbo].[PayerDetails] p where Id = @PayerId

	INSERT INTO ImportDetails (AccountNo,ImportSummaryId,TINCheckStatus,TINCheckRemarks,SubmissionSummaryId,TINType,TIN,
	PayerOfficeCode,GrossAmount,CNPTransactionAmount,FederalWithHoldingAmount,
	JanuaryAmount,FebruaryAmount,MarchAmount,AprilAmount,MayAmount,JuneAmount,JulyAmount,AugustAmount,
	SeptemberAmount,OctoberAmount,NovemberAmount,DecemberAmount,ForeignCountryIndicator,FirstPayeeName,
	SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZipCode,SecondTINNoticed,FillerIndicatorType,
	PaymentIndicatorType,TransactionCount,MerchantId,MerchantCategoryCode,SpecialDataEntry,StateWithHolding,
	LocalWithHolding,CFSF,IsActive,DateAdded)

	SELECT S.PayeeAccountNumber,@SummaryId,NULL,NULL,NULL,D.TINType,D.PayeeTIN,
	D.PayeeOfficeCode,S.GrossAmount,S.TotalCPAmount,NULL,
	S.JANUARY,S.FEBRUARY,S.MARCH,S.APRIL,S.MAY,S.JUNE,S.JULY,S.AUGUST,
	S.SEPTEMBER,S.OCTOBOR,S.NOVEMBER,S.DECEMBER,NULL,SUBSTRING(D.[PayeeFirstName],1,40), 
	SUBSTRING(D.[PayeeSecondName],1,40),SUBSTRING(D.[PayeeMailingAddress],1,40),SUBSTRING(D.[PayeeCity],1,40),D.[PayeeState],REPLACE(D.[PayeeZIP],'-',''),null,D.[FilerIndicatorType], 
	D.[PaymentIndicatorType],S.TotalTransaction,D.Id,D.[MCC],NULL,NULL,
	NULL,D.CFSF,1,GETDATE()

	FROM @K1099SUMMARYCHART S
	--LEFT JOIN #SUBMITTED O ON S.PayeeAccountNumber = O.AccountNo
	INNER JOIN  [dbo].[MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PaymentYear = @YEAR AND D.PayerId = @PayerId
	WHERE S.TransactionYear = @YEAR --AND O.Id IS NULL


	SET @ProcessLog = @ProcessLog + 'Account associated with '+@PAYERNAME+':'+CAST(@@ROWCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)

	DECLARE @ORPHANT NVARCHAR(1024),@ORPHANTCOUNT INT

	SELECT @ORPHANTCOUNT = COUNT(*)
	FROM @K1099SUMMARYCHART S
	LEFT JOIN  [dbo].[MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PaymentYear = @YEAR AND D.PayerId = @PayerId
	WHERE S.TransactionYear = @YEAR AND D.Id IS NULL


	IF @ORPHANTCOUNT>0
	BEGIN
		

		SET @ProcessLog = @ProcessLog + 'Account not associated with '+@PAYERNAME+':'+CAST(@ORPHANTCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)
	

		--SELECT @ORPHANT = ISNULL(@ORPHANT,'')+S.PayeeAccountNumber +CHAR(13)+CHAR(10)
		--FROM @K1099SUMMARYCHART S
		--LEFT JOIN  [dbo].[MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		--	AND D.IsActive = 1 AND D.PaymentYear = @YEAR AND D.PayerId = @PayerId
		--WHERE S.TransactionYear = @YEAR AND D.Id IS NULL

		--SET @ProcessLog = @ProcessLog + ISNULL(@ORPHANT,'')

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
	
	IF OBJECT_ID('tempdb..#TEMP_SUMMARY') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY
	END
	
	IF OBJECT_ID('tempdb..#SUBMITTED') IS NOT NULL
	BEGIN
	DROP TABLE #SUBMITTED
	END
	
RETURN