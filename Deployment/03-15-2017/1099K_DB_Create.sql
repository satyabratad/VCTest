
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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PreImportDataProcessing')
DROP PROCEDURE PreImportDataProcessing
GO
CREATE PROCEDURE [dbo].[PreImportDataProcessing]
	
AS
	-- CLEAR ALL RECORD
	TRUNCATE TABLE RawTransactionStagings

RETURN 
GO
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
GO