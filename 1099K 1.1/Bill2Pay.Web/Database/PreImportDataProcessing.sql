﻿IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PreImportDataProcessing')
DROP PROCEDURE PreImportDataProcessing
GO
CREATE PROCEDURE [PreImportDataProcessing]
	
AS
	-- CLEAR ALL RECORD
	TRUNCATE TABLE RawTransactionStagings

RETURN 
