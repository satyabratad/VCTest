/****** Object:  StoredProcedure [dbo].[ap_InsertPropertyAddress]    Script Date: 4/20/2017 7:05:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RS Software
-- Create date: 4/20/2017
-- Description:	Insert Property Address 
-- =============================================
ALTER PROCEDURE [dbo].[ap_InsertPropertyAddress] 
(
	@BatchID varchar(40),
	@ClientCode varchar(10),
	@ProductName varchar(40),
	@AccountNumber1 varchar(40),
	@AccountNumber2 varchar(40)= '',
	@AccountNumber3 varchar(40)= '',
	@Address1 varchar(40)=NULL,
	@Address2 varchar(40)=NULL,
	@State varchar(40)=NULL,
	@City varchar(40)=NULL,
	@ZipCode varchar(40)=NULL,
	@CreateDateEST DateTime2
)
AS
BEGIN TRY

	INSERT INTO [dbo].[PropertyAddress]
           ([Batch_ID]
           ,[ClientCode]
           ,[ProductName]
           ,[AccountNumber1]
           ,[AccountNumber2]
           ,[AccountNumber3]
           ,[Address1]
           ,[Address2]
           ,[State]
           ,[City]
           ,[ZipCode]
		   ,[CreateDateEST])
     VALUES
           (   @BatchID,
			   @ClientCode,
			   @ProductName, 
			   ISNULL(@AccountNumber1,''), 
			   ISNULL(@AccountNumber2,''),
			   ISNULL(@AccountNumber3,''),
			   @Address1, 
			   @Address2, 
			   @State, 
			   @City, 
			   @ZipCode,
			   @CreateDateEST
		   )
		
	RETURN 1

END TRY

BEGIN CATCH
	
    DECLARE @ErrorMessage NVARCHAR(4000)
    DECLARE @ErrorSeverity INT
    DECLARE @ErrorState INT

    SELECT @ErrorMessage = ERROR_MESSAGE(),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE()
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)  
    
    RETURN -1 

END CATCH
Go