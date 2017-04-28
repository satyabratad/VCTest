If Not Exists (Select * From [dbo].[Sysobjects] Where Upper(Name) = Upper('PropertyAddress') And Type = 'U')
Begin
	Create Table [dbo].[PropertyAddress]
	(
		PropertyAddress_ID  Int Identity (1,1) NOT NULL,
		Batch_ID			Int NOT NULL,
		ClientCode			Varchar(10) NOT NULL,
		ProductName			Varchar(40) NOT NULL,
		AccountNumber1		Varchar(40) NOT NULL,
		AccountNumber2		Varchar(40),
		AccountNumber3		Varchar(40),
		Address1			Nvarchar(40),
		Address2			Nvarchar(40),
		State				Nvarchar(40),
		City				Nvarchar(40),
		ZipCode				Nvarchar(10),
		CreateDateEST		DateTime2 
		CONSTRAINT PK_PropertyAddress PRIMARY KEY (PropertyAddress_ID)
	)

	
End

Go

If Not Exists (Select * From [dbo].[Sysobjects] Where Upper(Name) = Upper('FK_ClientProduct_PropertyAddress') And Type = 'F')
Begin

	Alter Table [dbo].[PropertyAddress]
	Add Constraint FK_ClientProduct_PropertyAddress Foreign Key (ProductName,ClientCode) 
	REFERENCES Products(ProductName,ClientCode) 

End
Go

If Exists (Select * From [dbo].[Sysobjects] Where Upper(Name) = Upper('ap_GetPropertyAddress') And Type = 'P')
Begin
	Drop Procedure [dbo].[ap_GetPropertyAddress]
End
Go
/****** Object:  StoredProcedure [dbo].[ap_GetPropertyAddress]    Script Date: 4/20/2017 7:05:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:RS Software
-- Create date: 4/20/2017
-- Description:	Get Property Address by Batch ID, Client Code and Product Name
-- =============================================
CREATE PROCEDURE [dbo].[ap_GetPropertyAddress] 
(
	@BatchID varchar(40),
	@ClientCode varchar(10)	
)
AS
BEGIN TRY

	SELECT [PropertyAddress_ID]
      ,[Batch_ID]
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
	  ,[CreateDateEST]
  FROM [dbo].[PropertyAddress]
  WHERE [Batch_ID]=@BatchID AND [ClientCode]=@ClientCode 
		
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

If Exists (Select * From [dbo].[Sysobjects] Where Upper(Name) = Upper('ap_InsertPropertyAddress') And Type = 'P')
Begin
	Drop Procedure [dbo].[ap_InsertPropertyAddress]
End
Go
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
CREATE PROCEDURE [dbo].[ap_InsertPropertyAddress] 
(
	@BatchID varchar(40),
	@ClientCode varchar(10),
	@ProductName varchar(40),
	@AccountNumber1 varchar(40),
	@AccountNumber2 varchar(40)= NULL,
	@AccountNumber3 varchar(40)= NULL,
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
			   @AccountNumber1, 
			   @AccountNumber2, 
			   @AccountNumber3,
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