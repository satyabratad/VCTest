IF EXISTS (SELECT * FROM sys.objects WHERE type = 'TF' AND name = 'ImportDataSummary')
DROP FUNCTION ImportDataSummary
GO

-- =============================================
-- Author:		RS Software
-- Create date: 03/10/2017
-- Description:	Generating Import Summary data 
-- =============================================
CREATE FUNCTION [ImportDataSummary]
(
	@PAYERID INT,
	@YEAR INT
)
RETURNS @K1099SUMMARYCHART TABLE(
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
		OCTOBOR DECIMAL(19,2),
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
	 
AS

BEGIN
	DECLARE @TEMPSUMMARY TABLE(
		TransactionYear INT,
		TransactionMonth  INT,
		[PayeeAccountNumber] NVARCHAR(256),
		TransactionAmount DECIMAL(19,2),
		TransactionType NVARCHAR(50),
		TransactionCount INT
	)

	INSERT INTO @TEMPSUMMARY
	SELECT TransactionYear,TransactionMonth,[PayeeAccountNumber],SUM(TransactionAmount) AS TransactionAmount,TransactionType,COUNT(1) AS TransactionCount
	
	FROM 
		(SELECT [PayeeAccountNumber],YEAR(TransactionDate) AS TransactionYear,
		MONTH(TransactionDate) AS TransactionMonth,TransactionAmount,TransactionType
		FROM [RawTransactions] WHERE IsActive = 1
	) P
	
	GROUP BY 
	TransactionYear,TransactionMonth,[PayeeAccountNumber],TransactionType
	ORDER BY 
	TransactionMonth,PayeeAccountNumber
	

	INSERT INTO @K1099SUMMARYCHART(TransactionYear,PayeeAccountNumber
	,JANUARY,FEBRUARY,MARCH,APRIL,MAY,JUNE,JULY,AUGUST,SEPTEMBER,OCTOBOR,NOVEMBER,DECEMBER)
	SELECT TransactionYear,
	PIV.PayeeAccountNumber,
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
	FROM 
	(
	  SELECT TransactionYear,TransactionMonth,PayeeAccountNumber,TransactionAmount
	  FROM @TEMPSUMMARY
	) src
	pivot
	(
	  SUM(TransactionAmount) 
	  FOR TransactionMonth in ([1], [2], [3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
	) PIV
	INNER JOIN [MerchantDetails] M ON M.PayeeAccountNumber = PIV.PayeeAccountNumber AND M.IsActive = 1
	INNER JOIN [PayerDetails] P ON P.ID = M.PayerId AND P.IsActive = 1 AND P.ID = @PAYERID
	WHERE TransactionYear = @YEAR


	UPDATE CHART SET
		CHART.GrossAmount = GOSS.TransactionAmount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
		SELECT TransactionYear,PayeeAccountNumber,SUM(TransactionAmount) AS TransactionAmount
			FROM @TEMPSUMMARY
			GROUP BY TransactionYear,PayeeAccountNumber
		)GOSS ON GOSS.TransactionYear= CHART.TransactionYear 
		AND GOSS.PayeeAccountNumber = CHART.PayeeAccountNumber 

	UPDATE CHART SET
		CHART.TotalCPAmount = CNP.TransactionAmount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
			SELECT TransactionYear,PayeeAccountNumber,TransactionType, SUM(TransactionAmount) AS TransactionAmount
			FROM @TEMPSUMMARY
			GROUP BY TransactionYear,PayeeAccountNumber,TransactionType
		)CNP ON CNP.TransactionYear= CHART.TransactionYear 
		AND CNP.PayeeAccountNumber = CHART.PayeeAccountNumber AND CNP.TransactionType = 'CNP'
	
	UPDATE CHART SET
		CHART.TotalTransaction = C.TransactionCount
	FROM @K1099SUMMARYCHART CHART
	LEFT JOIN (
		SELECT TransactionYear,PayeeAccountNumber,SUM(TransactionCount) AS TransactionCount
		FROM @TEMPSUMMARY
		GROUP BY TransactionYear,PayeeAccountNumber
	)C ON C.TransactionYear= CHART.TransactionYear 
		AND C.PayeeAccountNumber = CHART.PayeeAccountNumber

	UPDATE CHART SET
		CHART.ImportDetailsId = EXT.ImportDetailsId, 
		CHART.SubmissionSummaryId = EXT.SubmissionSummaryId ,
		CHART.SubmissionStatusId = EXT.SubmissionStatusId ,
		CHART.StatusId  = EXT.StatusId ,
		CHART.TINCheckStatus = EXT.TINCheckStatus,
		CHART.TINCheckRemarks = EXT.TINCheckRemarks
		
		
	FROM @K1099SUMMARYCHART CHART
	INNER JOIN (
	SELECT D.AccountNumber,
		D.Id ImportDetailsId, 
		D.SubmissionSummaryId ,
		SS.Id SubmissionStatusId,
		SS.StatusId ,
		D.TINCheckStatus,
		D.TINCheckRemarks FROM [ImportDetails] D 
	INNER JOIN [ImportSummaries] S ON S.Id = D.ImportSummaryId AND S.IsActive = 1 AND S.PaymentYear = @YEAR
	INNER JOIN [MerchantDetails] M ON M.ID = D.MerchantId AND M.IsActive = 1
	INNER JOIN [PayerDetails] P ON P.ID = M.PayerId AND P.IsActive = 1 AND P.ID = @PAYERID
	LEFT JOIN [SubmissionStatus] SS ON D.AccountNumber = SS.AccountNumber AND SS.IsActive =1
	WHERE D.IsActive =1   
	) EXT ON CHART.PayeeAccountNumber = EXT.AccountNumber 

	RETURN
END

GO
