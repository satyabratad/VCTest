IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PreImportDataProcessing')
DROP PROCEDURE PreImportDataProcessing
GO

-- =============================================
-- Author:		RS Software
-- Create date: 03/10/2017
-- Description:	Clearing all record from the staging table during transaction import 
-- =============================================
CREATE PROCEDURE [PreImportDataProcessing]
	
AS
	-- CLEAR ALL RECORD
	DELETE FROM RawTransactionStagings


RETURN 
