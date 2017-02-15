CREATE PROCEDURE [dbo].[PostImportDataProcessing]
	@YEAR INT = 2016,
	@FileName varchar(255),
	@UserId bigint
AS
	
	declare @PSEId int =1;

	IF OBJECT_ID('tempdb..#TEMP_SUMMARY') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY
	END
	IF OBJECT_ID('tempdb..##TEMP_SUMMARY_PIVOT') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY_PIVOT
	END
	IF OBJECT_ID('tempdb..##TEMP_SUMMARY_GROSS') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY_GROSS
	END
	IF OBJECT_ID('tempdb..##TEMP_SUMMARY_CP') IS NOT NULL
	BEGIN
	DROP TABLE #TEMP_SUMMARY_CP
	END
	

	-- ARCHIVE EXISTING DATA
	UPDATE [dbo].[RawTransaction] SET Isactive = 0,UpdatedDate = getdate() WHERE Isactive=1
	--DELETE FROM [dbo].[RawTransaction]

	-- INSERT NEW DATA
	INSERT INTO [dbo].[RawTransaction](PayeeAccountNumber,TransactionType,TransactionAmount,TransactionDate,IsActive,UserID,[AddedDate])
	SELECT 
	[PayeeAccountNumber], 
	CASE WHEN [TransactionType] = 7 THEN 'CNP' ELSE 'CP' END AS [TransactionType],
	CAST([TransactionAmount] AS DECIMAL(19,2)) AS TransactionAmount, 
	CAST([TransactionDate] AS DATE) AS TransactionDate,
	1,
	@UserId,
	getdate()
	FROM [dbo].[RawTransactionStagings]

	---- ARCHIVE EXISTING DATA
	--UPDATE [dbo].[MerchantDetails] SET Isactive = 0,UpdatedDate = getdate() WHERE Isactive=1
	----DELETE FROM [dbo].[MerchantDetails]

	------ DETAILS
	--INSERT INTO [dbo].[MerchantDetails] (PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,
	--PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,AddedDate,UserId)
	--SELECT DISTINCT 
	--[PayeeAccountNumber],
	--[TINType], 
	--[PayeeTIN], 
	--[PayeeOfficeCode], 
	----[CardPresentTransactions], 
	----[FederalIncomeTaxWithheld], 
	----[StateIncomeTaxWithheld], 
	--[PayeeFirstName], 
	--[PayeeSecondName], 
	--[PayeeMailingAddress], 
	--[PayeeCity], 
	--[PayeeState],
	--[PayeeZIP], 
	--[FilerIndicatorType], 
	--[PaymentIndicatorType], 
	--[MCC],
	--1,
	--GETDATE()
	--,@UserId
	--FROM [dbo].[RawTransactionStagings]


	-- GROUP BY MONTH,YEAR
	SELECT TransactionYear,TransactionMonth,[PayeeAccountNumber],SUM(TransactionAmount) AS TransactionAmount,TransactionType
	INTO #TEMP_SUMMARY 
	FROM 
		(SELECT [PayeeAccountNumber],YEAR(TransactionDate) AS TransactionYear,
		MONTH(TransactionDate) AS TransactionMonth,TransactionAmount,TransactionType
		FROM [dbo].[RawTransaction] where IsActive = 1
	) P
	
	GROUP BY 
	TransactionYear,TransactionMonth,[PayeeAccountNumber],TransactionType
	ORDER BY 
	TransactionMonth,PayeeAccountNumber

	select TransactionYear,PayeeAccountNumber,TransactionType, sum(TransactionAmount) as TransactionAmount
	into #TEMP_SUMMARY_CP
	from #TEMP_SUMMARY
	group by TransactionYear,PayeeAccountNumber,TransactionType
	
	select TransactionYear,PayeeAccountNumber, sum(TransactionAmount) as TransactionAmount
	into #TEMP_SUMMARY_GROSS
	from #TEMP_SUMMARY_CP
	group by TransactionYear,PayeeAccountNumber

	

	-- PIVOT
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
	INTO #TEMP_SUMMARY_PIVOT
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

	

	DECLARE @SummaryId int,@RecordCount int

	SELECT @RecordCount = COUNT(Id) FROM [dbo].[RawTransaction] WHERE IsActive = 1

	INSERT INTO ImportSummaries(PaymentYear,ImportDate,[FileName],RecordCount,UserId)
	values (@YEAR,GETDATE(),@FileName,@RecordCount,@UserId)
	SET @SummaryId = @@IDENTITY


	SELECT TOP 1 @PSEID = ID  FROM [dbo].[PSEMasters]

	-- CLEAR EXISTING DATA
	DELETE FROM ImportDetails

	INSERT INTO ImportDetails (AccountNo,ImportSummaryId,TINCheckStatus,TINCheckRemarks,SubmissionSummaryId,TINType,TIN,
	PayerOfficeCode,GrossAmount,CNPTransactionAmount,FederalWithHoldingAmount,
	JanuaryAmount,FebruaryAmount,MarchAmount,AprilAmount,MayAmount,JuneAmount,JulyAmount,AugustAmount,
	SeptemberAmount,OctoberAmount,NovemberAmount,DecemberAmount,ForeignCountryIndicator,FirstPayeeName,
	SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZipCode,SecondTINNoticed,FillerIndicatorType,
	PaymentIndicatorType,TransactionCount,PSEMasterId,MerchantCategoryCode,SpecialDataEntry,StateWithHolding,
	LocalWithHolding,CFSF)

	SELECT S.PayeeAccountNumber,@SummaryId,null,null,null,D.TINType,D.PayeeTIN,
	D.PayeeOfficeCode,G.TransactionAmount,C.TransactionAmount,0,
	S.JANUARY,S.FEBRUARY,S.MARCH,S.APRIL,S.MAY,S.JUNE,S.JULY,S.AUGUST,
	S.SEPTEMBER,S.OCTOBOR,S.NOVEMBER,S.DECEMBER,null,D.[PayeeFirstName], 
	D.[PayeeSecondName],D.[PayeeMailingAddress],D.[PayeeCity],D.[PayeeState],Replace(D.[PayeeZIP],'-',''),null,D.[FilerIndicatorType], 
	D.[PaymentIndicatorType],0,@PSEId,D.[MCC],null,null,
	null,null

	FROM [dbo].[MerchantDetails] D
	LEFT JOIN #TEMP_SUMMARY_PIVOT S ON S.PayeeAccountNumber = D.PayeeAccountNumber and D.IsActive = 1 and S.TransactionYear = @YEAR
	LEFT JOIN #TEMP_SUMMARY_GROSS G ON G.TransactionYear= S.TransactionYear AND G.PayeeAccountNumber = S.PayeeAccountNumber and G.TransactionYear = @YEAR
	LEFT JOIN #TEMP_SUMMARY_CP C ON G.TransactionYear= S.TransactionYear AND C.PayeeAccountNumber = S.PayeeAccountNumber AND C.TransactionType = 'CNP'and  C.TransactionYear = @YEAR
	
	DROP TABLE #TEMP_SUMMARY
	DROP TABLE #TEMP_SUMMARY_PIVOT
	DROP TABLE #TEMP_SUMMARY_GROSS
	DROP TABLE #TEMP_SUMMARY_CP

	--select * from ImportSummaries
	--select * from ImportDetails
	
RETURN