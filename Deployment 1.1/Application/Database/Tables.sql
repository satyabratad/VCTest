
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsDefaultPasswordChanged] [bit] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImportDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[ImportSummaryId] [int] NOT NULL,
	[TINCheckStatus] [nvarchar](2) NULL,
	[TINCheckRemarks] [nvarchar](512) NULL,
	[SubmissionSummaryId] [int] NULL,
	[TINType] [nvarchar](1) NULL,
	[TIN] [nvarchar](9) NULL,
	[PayerOfficeCode] [nvarchar](4) NULL,
	[GrossAmount] [decimal](18, 2) NULL,
	[CNPTransactionAmount] [decimal](18, 2) NULL,
	[FederalWithHoldingAmount] [decimal](18, 2) NULL,
	[JanuaryAmount] [decimal](18, 2) NULL,
	[FebruaryAmount] [decimal](18, 2) NULL,
	[MarchAmount] [decimal](18, 2) NULL,
	[AprilAmount] [decimal](18, 2) NULL,
	[MayAmount] [decimal](18, 2) NULL,
	[JuneAmount] [decimal](18, 2) NULL,
	[JulyAmount] [decimal](18, 2) NULL,
	[AugustAmount] [decimal](18, 2) NULL,
	[SeptemberAmount] [decimal](18, 2) NULL,
	[OctoberAmount] [decimal](18, 2) NULL,
	[NovemberAmount] [decimal](18, 2) NULL,
	[DecemberAmount] [decimal](18, 2) NULL,
	[ForeignCountryIndicator] [nvarchar](1) NULL,
	[FirstPayeeName] [nvarchar](40) NULL,
	[SecondPayeeName] [nvarchar](40) NULL,
	[PayeeMailingAddress] [nvarchar](40) NULL,
	[PayeeCity] [nvarchar](40) NULL,
	[PayeeState] [nvarchar](2) NULL,
	[PayeeZipCode] [nvarchar](9) NULL,
	[SecondTINNoticed] [nvarchar](1) NULL,
	[FillerIndicatorType] [nvarchar](1) NULL,
	[PaymentIndicatorType] [nvarchar](1) NULL,
	[TransactionCount] [int] NOT NULL,
	[PSEDetails_Id] [int] NULL,
	[MerchantCategoryCode] [nvarchar](4) NULL,
	[SpecialDataEntry] [nvarchar](60) NULL,
	[StateWithHolding] [decimal](18, 2) NULL,
	[LocalWithHolding] [decimal](18, 2) NULL,
	[CFSF] [nvarchar](2) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[NameControl] [nvarchar](4) NULL,
	[MerchantId] [int] NULL,
 CONSTRAINT [PK_dbo.ImportDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImportSummaries]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportSummaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentYear] [int] NOT NULL,
	[ImportDate] [datetime] NOT NULL,
	[FileName] [nvarchar](100) NULL,
	[RecordCount] [int] NULL,
	[ProcessLog] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.ImportSummaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MerchantDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MerchantDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayeeAccountNumber] [nvarchar](255) NULL,
	[TINType] [nvarchar](255) NULL,
	[PayeeTIN] [nvarchar](255) NULL,
	[PayeeOfficeCode] [nvarchar](255) NULL,
	[PayeeFirstName] [nvarchar](255) NULL,
	[PayeeSecondName] [nvarchar](255) NULL,
	[PayeeMailingAddress] [nvarchar](255) NULL,
	[PayeeCity] [nvarchar](255) NULL,
	[PayeeState] [nvarchar](255) NULL,
	[PayeeZIP] [nvarchar](255) NULL,
	[FilerIndicatorType] [nvarchar](255) NULL,
	[PaymentIndicatorType] [nvarchar](255) NULL,
	[MCC] [nvarchar](255) NULL,
	[CFSF] [nvarchar](2) NULL,
	[PayerId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PaymentYear] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MerchantDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PayerDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayerDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CFSF] [nvarchar](1) NULL,
	[PayerTIN] [nvarchar](9) NULL,
	[PayerNameControl] [nvarchar](4) NULL,
	[LastFilingIndicator] [nvarchar](1) NULL,
	[ReturnType] [nvarchar](2) NULL,
	[AmountCodes] [nvarchar](16) NULL,
	[PayerForeignEntityIndicator] [nvarchar](1) NULL,
	[FirstPayerName] [nvarchar](40) NULL,
	[SecondPayerName] [nvarchar](40) NULL,
	[TransferAgentIndicator] [nvarchar](1) NULL,
	[PayerShippingAddress] [nvarchar](40) NULL,
	[PayerCity] [nvarchar](40) NULL,
	[PayerState] [nvarchar](2) NULL,
	[PayerZIP] [nvarchar](9) NULL,
	[PayerTelephoneNumber] [nvarchar](15) NULL,
	[TransmitterId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[PaymentYear] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.PayerDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PSEDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PSEDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransmitterTIN] [nvarchar](9) NULL,
	[TransmitterControlCode] [nvarchar](5) NULL,
	[TestFileIndicator] [nvarchar](1) NULL,
	[TransmitterForeignEntityIndicator] [nvarchar](1) NULL,
	[TransmitterName] [nvarchar](40) NULL,
	[TransmitterNameContinued] [nvarchar](40) NULL,
	[CompanyName] [nvarchar](40) NULL,
	[CompanyNameContinued] [nvarchar](40) NULL,
	[CompanyMailingAddress] [nvarchar](40) NULL,
	[CompanyCity] [nvarchar](40) NULL,
	[CompanyState] [nvarchar](2) NULL,
	[CompanyZIP] [nvarchar](9) NULL,
	[TotalNumberofPayees] [int] NOT NULL,
	[ContactName] [nvarchar](40) NULL,
	[ContactTelephoneNumber] [nvarchar](15) NULL,
	[ContactEmailAddress] [nvarchar](50) NULL,
	[VendorIndicator] [nvarchar](1) NULL,
	[VendorName] [nvarchar](40) NULL,
	[VendorMailingAddress] [nvarchar](40) NULL,
	[VendorCity] [nvarchar](40) NULL,
	[VendorState] [nvarchar](2) NULL,
	[VendorZIP] [nvarchar](9) NULL,
	[VendorContactName] [nvarchar](40) NULL,
	[VendorContactTelephoneNumber] [nvarchar](15) NULL,
	[VendorForeignEntityIndicator] [nvarchar](1) NULL,
	[CFSF] [nvarchar](1) NULL,
	[PayerTIN] [nvarchar](9) NULL,
	[PayerNameControl] [nvarchar](4) NULL,
	[LastFilingIndicator] [nvarchar](1) NULL,
	[ReturnType] [nvarchar](2) NULL,
	[AmountCodes] [nvarchar](16) NULL,
	[PayerForeignEntityIndicator] [nvarchar](1) NULL,
	[FirstPayerName] [nvarchar](40) NULL,
	[SecondPayerName] [nvarchar](40) NULL,
	[TransferAgentIndicator] [nvarchar](1) NULL,
	[PayerShippingAddress] [nvarchar](40) NULL,
	[PayerCity] [nvarchar](40) NULL,
	[PayerState] [nvarchar](2) NULL,
	[PayerZIP] [nvarchar](9) NULL,
	[PayerTelephoneNumber] [nvarchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PSEDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RawTransactions]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayeeAccountNumber] [nvarchar](256) NULL,
	[TransactionAmount] [decimal](18, 2) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionType] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.RawTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RawTransactionStagings]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawTransactionStagings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TINType] [nvarchar](255) NULL,
	[PayeeTIN] [nvarchar](255) NULL,
	[PayeeAccountNumber] [nvarchar](255) NULL,
	[PayeeOfficeCode] [nvarchar](255) NULL,
	[CardPresentTransactions] [nvarchar](255) NULL,
	[FederalIncomeTaxWithheld] [nvarchar](255) NULL,
	[StateIncomeTaxWithheld] [nvarchar](255) NULL,
	[TransactionAmount] [nvarchar](255) NULL,
	[TransactionDate] [nvarchar](255) NULL,
	[TransactionType] [nvarchar](255) NULL,
	[PayeeFirstName] [nvarchar](255) NULL,
	[PayeeSecondName] [nvarchar](255) NULL,
	[PayeeMailingAddress] [nvarchar](255) NULL,
	[PayeeCity] [nvarchar](255) NULL,
	[PayeeState] [nvarchar](255) NULL,
	[PayeeZIP] [nvarchar](255) NULL,
	[FilerIndicatorType] [nvarchar](255) NULL,
	[PaymentIndicatorType] [nvarchar](255) NULL,
	[MCC] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.RawTransactionStagings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubmissionDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[SubmissionId] [int] NOT NULL,
	[SubmissionType] [int] NOT NULL,
	[TINType] [nvarchar](1) NULL,
	[TIN] [nvarchar](9) NULL,
	[PayerOfficeCode] [nvarchar](4) NULL,
	[GrossAmount] [decimal](18, 2) NULL,
	[CNPTransactionAmount] [decimal](18, 2) NULL,
	[FederalWithHoldingAmount] [decimal](18, 2) NULL,
	[JanuaryAmount] [decimal](18, 2) NULL,
	[FebruaryAmount] [decimal](18, 2) NULL,
	[MarchAmount] [decimal](18, 2) NULL,
	[AprilAmount] [decimal](18, 2) NULL,
	[MayAmount] [decimal](18, 2) NULL,
	[JuneAmount] [decimal](18, 2) NULL,
	[JulyAmount] [decimal](18, 2) NULL,
	[AugustAmount] [decimal](18, 2) NULL,
	[SeptemberAmount] [decimal](18, 2) NULL,
	[OctoberAmount] [decimal](18, 2) NULL,
	[NovemberAmount] [decimal](18, 2) NULL,
	[DecemberAmount] [decimal](18, 2) NULL,
	[ForeignCountryIndicator] [nvarchar](1) NULL,
	[FirstPayeeName] [nvarchar](40) NULL,
	[SecondPayeeName] [nvarchar](40) NULL,
	[PayeeMailingAddress] [nvarchar](40) NULL,
	[PayeeCity] [nvarchar](40) NULL,
	[PayeeState] [nvarchar](2) NULL,
	[PayeeZipCode] [nvarchar](9) NULL,
	[SecondTINNoticed] [nvarchar](1) NULL,
	[FillerIndicatorType] [nvarchar](1) NULL,
	[PaymentIndicatorType] [nvarchar](1) NULL,
	[TransactionCount] [int] NOT NULL,
	[PseId] [int] NULL,
	[MerchantCategoryCode] [nvarchar](4) NULL,
	[SpecialDataEntry] [nvarchar](60) NULL,
	[StateWithHolding] [decimal](18, 2) NULL,
	[LocalWithHolding] [decimal](18, 2) NULL,
	[CFSF] [nvarchar](2) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[NameControl] [nvarchar](4) NULL,
	[MerchantId] [int] NULL,
 CONSTRAINT [PK_dbo.SubmissionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubmissionStatus]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentYear] [int] NOT NULL,
	[AccountNumber] [nvarchar](255) NULL,
	[ProcessingDate] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.SubmissionStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubmissionSummaries]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionSummaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentYear] [int] NOT NULL,
	[SubmissionDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Status_Id] [int] NULL,
 CONSTRAINT [PK_dbo.SubmissionSummaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TINStatus]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TINStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.TINStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransmitterDetails]    Script Date: 2/28/2017 4:48:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransmitterDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransmitterTIN] [nvarchar](9) NULL,
	[TransmitterControlCode] [nvarchar](5) NULL,
	[TestFileIndicator] [nvarchar](1) NULL,
	[TransmitterForeignEntityIndicator] [nvarchar](1) NULL,
	[TransmitterName] [nvarchar](40) NULL,
	[TransmitterNameContinued] [nvarchar](40) NULL,
	[CompanyName] [nvarchar](40) NULL,
	[CompanyNameContinued] [nvarchar](40) NULL,
	[CompanyMailingAddress] [nvarchar](40) NULL,
	[CompanyCity] [nvarchar](40) NULL,
	[CompanyState] [nvarchar](2) NULL,
	[CompanyZIP] [nvarchar](9) NULL,
	[TotalNumberofPayees] [int] NOT NULL,
	[ContactName] [nvarchar](40) NULL,
	[ContactTelephoneNumber] [nvarchar](15) NULL,
	[ContactEmailAddress] [nvarchar](50) NULL,
	[VendorIndicator] [nvarchar](1) NULL,
	[VendorName] [nvarchar](40) NULL,
	[VendorMailingAddress] [nvarchar](40) NULL,
	[VendorCity] [nvarchar](40) NULL,
	[VendorState] [nvarchar](2) NULL,
	[VendorZIP] [nvarchar](9) NULL,
	[VendorContactName] [nvarchar](40) NULL,
	[VendorContactTelephoneNumber] [nvarchar](15) NULL,
	[VendorForeignEntityIndicator] [nvarchar](1) NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[PaymentYear] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.TransmitterDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MerchantDetails] ADD  DEFAULT ((0)) FOR [PaymentYear]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ImportDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId] FOREIGN KEY([ImportSummaryId])
REFERENCES [dbo].[ImportSummaries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImportDetails] CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId]
GO
ALTER TABLE [dbo].[ImportDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId] FOREIGN KEY([MerchantId])
REFERENCES [dbo].[MerchantDetails] ([Id])
GO
ALTER TABLE [dbo].[ImportDetails] CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId]
GO
ALTER TABLE [dbo].[ImportDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportDetails_dbo.PSEDetails_PseId] FOREIGN KEY([PSEDetails_Id])
REFERENCES [dbo].[PSEDetails] ([Id])
GO
ALTER TABLE [dbo].[ImportDetails] CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.PSEDetails_PseId]
GO
ALTER TABLE [dbo].[ImportDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId] FOREIGN KEY([SubmissionSummaryId])
REFERENCES [dbo].[SubmissionSummaries] ([Id])
GO
ALTER TABLE [dbo].[ImportDetails] CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId]
GO
ALTER TABLE [dbo].[ImportSummaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportSummaries_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImportSummaries] CHECK CONSTRAINT [FK_dbo.ImportSummaries_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[MerchantDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MerchantDetails_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MerchantDetails] CHECK CONSTRAINT [FK_dbo.MerchantDetails_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[MerchantDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MerchantDetails_dbo.PayerDetails_PayerId] FOREIGN KEY([PayerId])
REFERENCES [dbo].[PayerDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MerchantDetails] CHECK CONSTRAINT [FK_dbo.MerchantDetails_dbo.PayerDetails_PayerId]
GO
ALTER TABLE [dbo].[PayerDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PayerDetails_dbo.TransmitterDetails_TransmitterId] FOREIGN KEY([TransmitterId])
REFERENCES [dbo].[TransmitterDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PayerDetails] CHECK CONSTRAINT [FK_dbo.PayerDetails_dbo.TransmitterDetails_TransmitterId]
GO
ALTER TABLE [dbo].[RawTransactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RawTransactions_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RawTransactions] CHECK CONSTRAINT [FK_dbo.RawTransactions_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[SubmissionDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId] FOREIGN KEY([MerchantId])
REFERENCES [dbo].[MerchantDetails] ([Id])
GO
ALTER TABLE [dbo].[SubmissionDetails] CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId]
GO
ALTER TABLE [dbo].[SubmissionDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId] FOREIGN KEY([PseId])
REFERENCES [dbo].[PSEDetails] ([Id])
GO
ALTER TABLE [dbo].[SubmissionDetails] CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId]
GO
ALTER TABLE [dbo].[SubmissionDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[SubmissionSummaries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubmissionDetails] CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId]
GO
ALTER TABLE [dbo].[SubmissionStatus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionStatus_dbo.Status_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubmissionStatus] CHECK CONSTRAINT [FK_dbo.SubmissionStatus_dbo.Status_StatusId]
GO
ALTER TABLE [dbo].[SubmissionSummaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubmissionSummaries] CHECK CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[SubmissionSummaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.Status_Status_Id] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[SubmissionSummaries] CHECK CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.Status_Status_Id]
GO
