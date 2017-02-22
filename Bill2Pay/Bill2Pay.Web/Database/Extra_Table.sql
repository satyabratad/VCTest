
DROP TABLE [dbo].[RawTransaction] 
GO
CREATE TABLE [dbo].[RawTransaction] (
    [Id] [int] NOT NULL IDENTITY(1,1),
    [PayeeAccountNumber] [nvarchar](255),
    [TransactionAmount] DECIMAL(19,2),
    [TransactionDate] DATETIME,
    [TransactionType] [nvarchar](255),
    [IsActive] bit,
	[AddedDate] datetime not null,
	[UpdatedDate] datetime null,
	[UserId] bigint
	CONSTRAINT [PK_dbo.RawTransaction] PRIMARY KEY ([Id])
)
GO

DROP TABLE [dbo].[MerchantDetails]
GO
CREATE TABLE [dbo].[MerchantDetails] (
    [Id] [int] NOT NULL IDENTITY,
    [PayeeAccountNumber] [nvarchar](255),
	[TINType] [nvarchar](255),
    [PayeeTIN] [nvarchar](255),
    [PayeeOfficeCode] [nvarchar](255),
    [PayeeFirstName] [nvarchar](255),
    [PayeeSecondName] [nvarchar](255),
    [PayeeMailingAddress] [nvarchar](255),
    [PayeeCity] [nvarchar](255),
    [PayeeState] [nvarchar](255),
    [PayeeZIP] [nvarchar](255),
    [FilerIndicatorType] [nvarchar](255),
    [PaymentIndicatorType] [nvarchar](255),
    [MCC] [nvarchar](255),
	[IsActive] bit,
	[AddedDate] datetime not null,
	[UpdatedDate] datetime null,
	[UserId] bigint
    CONSTRAINT [PK_dbo.MerchantDetails] PRIMARY KEY ([Id])
)

BULK INSERT [TestImport]
FROM 'C:\Project\K1099\Bill2Pay.Web\App_Data\Uploads\Transactions\test.csv'
WITH(
FIRSTROW=1,
FIELDTERMINATOR = ',',
ROWTERMINATOR='\N',
ERRORFILE='C:\Project\K1099\Bill2Pay.Web\App_Data\Uploads\Transactions\Error.csv',
TABLOCK
)