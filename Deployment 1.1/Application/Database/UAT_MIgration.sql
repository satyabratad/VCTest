
PRINT N'Dropping unnamed constraint on [dbo].[MerchantDetails]...';


GO
ALTER TABLE [dbo].[MerchantDetails] DROP CONSTRAINT [DF__MerchantD__Payme__3864608B];


GO
PRINT N'Dropping unnamed constraint on [dbo].[PayerDetails]...';


GO
ALTER TABLE [dbo].[PayerDetails] DROP CONSTRAINT [DF__PayerDeta__Payme__25518C17];


GO
PRINT N'Dropping unnamed constraint on [dbo].[TransmitterDetails]...';


GO
ALTER TABLE [dbo].[TransmitterDetails] DROP CONSTRAINT [DF__Transmitt__Payme__37703C52];


GO
PRINT N'Dropping [dbo].[FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId]...';


GO
ALTER TABLE [dbo].[ImportDetails] DROP CONSTRAINT [FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId];


GO
PRINT N'Dropping [dbo].[FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId]...';


GO
ALTER TABLE [dbo].[ImportDetails] DROP CONSTRAINT [FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId];


GO
PRINT N'Dropping [dbo].[FK_dbo.ImportDetails_dbo.PSEDetails_PseId]...';


GO
ALTER TABLE [dbo].[ImportDetails] DROP CONSTRAINT [FK_dbo.ImportDetails_dbo.PSEDetails_PseId];


GO
PRINT N'Dropping [dbo].[FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId]...';


GO
ALTER TABLE [dbo].[ImportDetails] DROP CONSTRAINT [FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId];


GO
PRINT N'Dropping [dbo].[FK_dbo.RawTransactions_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[RawTransactions] DROP CONSTRAINT [FK_dbo.RawTransactions_dbo.AspNetUsers_UserId];


GO
PRINT N'Dropping [dbo].[FK_dbo.SubmissionStatus_dbo.Status_StatusId]...';


GO
ALTER TABLE [dbo].[SubmissionStatus] DROP CONSTRAINT [FK_dbo.SubmissionStatus_dbo.Status_StatusId];


GO
PRINT N'Dropping [dbo].[FK_dbo.SubmissionSummaries_dbo.Status_Status_Id]...';


GO
ALTER TABLE [dbo].[SubmissionSummaries] DROP CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.Status_Status_Id];


GO
PRINT N'Dropping [dbo].[FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] DROP CONSTRAINT [FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId];


GO
PRINT N'Dropping [dbo].[FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] DROP CONSTRAINT [FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId];


GO
PRINT N'Dropping [dbo].[FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] DROP CONSTRAINT [FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId];


GO
PRINT N'Starting rebuilding table [dbo].[ImportDetails]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ImportDetails] (
    [Id]                       INT             IDENTITY (1, 1) NOT NULL,
    [AccountNo]                NVARCHAR (20)   NULL,
    [ImportSummaryId]          INT             NOT NULL,
    [TINCheckStatus]           NVARCHAR (2)    NULL,
    [TINCheckRemarks]          NVARCHAR (512)  NULL,
    [SubmissionSummaryId]      INT             NULL,
    [TINType]                  NVARCHAR (1)    NULL,
    [TIN]                      NVARCHAR (9)    NULL,
    [PayerOfficeCode]          NVARCHAR (4)    NULL,
    [GrossAmount]              DECIMAL (18, 2) NULL,
    [CNPTransactionAmount]     DECIMAL (18, 2) NULL,
    [FederalWithHoldingAmount] DECIMAL (18, 2) NULL,
    [JanuaryAmount]            DECIMAL (18, 2) NULL,
    [FebruaryAmount]           DECIMAL (18, 2) NULL,
    [MarchAmount]              DECIMAL (18, 2) NULL,
    [AprilAmount]              DECIMAL (18, 2) NULL,
    [MayAmount]                DECIMAL (18, 2) NULL,
    [JuneAmount]               DECIMAL (18, 2) NULL,
    [JulyAmount]               DECIMAL (18, 2) NULL,
    [AugustAmount]             DECIMAL (18, 2) NULL,
    [SeptemberAmount]          DECIMAL (18, 2) NULL,
    [OctoberAmount]            DECIMAL (18, 2) NULL,
    [NovemberAmount]           DECIMAL (18, 2) NULL,
    [DecemberAmount]           DECIMAL (18, 2) NULL,
    [ForeignCountryIndicator]  NVARCHAR (1)    NULL,
    [FirstPayeeName]           NVARCHAR (40)   NULL,
    [SecondPayeeName]          NVARCHAR (40)   NULL,
    [PayeeMailingAddress]      NVARCHAR (40)   NULL,
    [PayeeCity]                NVARCHAR (40)   NULL,
    [PayeeState]               NVARCHAR (2)    NULL,
    [PayeeZipCode]             NVARCHAR (9)    NULL,
    [SecondTINNoticed]         NVARCHAR (1)    NULL,
    [FillerIndicatorType]      NVARCHAR (1)    NULL,
    [PaymentIndicatorType]     NVARCHAR (1)    NULL,
    [TransactionCount]         INT             NOT NULL,
    [MerchantCategoryCode]     NVARCHAR (4)    NULL,
    [SpecialDataEntry]         NVARCHAR (60)   NULL,
    [StateWithHolding]         DECIMAL (18, 2) NULL,
    [LocalWithHolding]         DECIMAL (18, 2) NULL,
    [CFSF]                     NVARCHAR (2)    NULL,
    [NameControl]              NVARCHAR (4)    NULL,
    [IsActive]                 BIT             NOT NULL,
    [DateAdded]                DATETIME2 (7)   NOT NULL,
    [MerchantId]               INT             NULL,
    [PSEDetails_Id]            INT             NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.ImportDetails1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ImportDetails])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ImportDetails] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ImportDetails] ([Id], [AccountNo], [ImportSummaryId], [TINCheckStatus], [TINCheckRemarks], [SubmissionSummaryId], [TINType], [TIN], [PayerOfficeCode], [GrossAmount], [CNPTransactionAmount], [FederalWithHoldingAmount], [JanuaryAmount], [FebruaryAmount], [MarchAmount], [AprilAmount], [MayAmount], [JuneAmount], [JulyAmount], [AugustAmount], [SeptemberAmount], [OctoberAmount], [NovemberAmount], [DecemberAmount], [ForeignCountryIndicator], [FirstPayeeName], [SecondPayeeName], [PayeeMailingAddress], [PayeeCity], [PayeeState], [PayeeZipCode], [SecondTINNoticed], [FillerIndicatorType], [PaymentIndicatorType], [TransactionCount], [PSEDetails_Id], [MerchantCategoryCode], [SpecialDataEntry], [StateWithHolding], [LocalWithHolding], [CFSF], [IsActive], [DateAdded], [NameControl], [MerchantId])
        SELECT   [Id],
                 [AccountNo],
                 [ImportSummaryId],
                 [TINCheckStatus],
                 [TINCheckRemarks],
                 [SubmissionSummaryId],
                 [TINType],
                 [TIN],
                 [PayerOfficeCode],
                 [GrossAmount],
                 [CNPTransactionAmount],
                 [FederalWithHoldingAmount],
                 [JanuaryAmount],
                 [FebruaryAmount],
                 [MarchAmount],
                 [AprilAmount],
                 [MayAmount],
                 [JuneAmount],
                 [JulyAmount],
                 [AugustAmount],
                 [SeptemberAmount],
                 [OctoberAmount],
                 [NovemberAmount],
                 [DecemberAmount],
                 [ForeignCountryIndicator],
                 [FirstPayeeName],
                 [SecondPayeeName],
                 [PayeeMailingAddress],
                 [PayeeCity],
                 [PayeeState],
                 [PayeeZipCode],
                 [SecondTINNoticed],
                 [FillerIndicatorType],
                 [PaymentIndicatorType],
                 [TransactionCount],
                 [PSEDetails_Id],
                 [MerchantCategoryCode],
                 [SpecialDataEntry],
                 [StateWithHolding],
                 [LocalWithHolding],
                 [CFSF],
                 [IsActive],
                 [DateAdded],
                 [NameControl],
                 [MerchantId]
        FROM     [dbo].[ImportDetails]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ImportDetails] OFF;
    END

DROP TABLE [dbo].[ImportDetails];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ImportDetails]', N'ImportDetails';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.ImportDetails1]', N'PK_dbo.ImportDetails', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[ImportDetails].[IX_ImportSummaryId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ImportSummaryId]
    ON [dbo].[ImportDetails]([ImportSummaryId] ASC);


GO
PRINT N'Creating [dbo].[ImportDetails].[IX_SubmissionSummaryId]...';


GO
CREATE NONCLUSTERED INDEX [IX_SubmissionSummaryId]
    ON [dbo].[ImportDetails]([SubmissionSummaryId] ASC);


GO
PRINT N'Creating [dbo].[ImportDetails].[IX_MerchantId]...';


GO
CREATE NONCLUSTERED INDEX [IX_MerchantId]
    ON [dbo].[ImportDetails]([MerchantId] ASC);


GO
PRINT N'Creating [dbo].[ImportDetails].[IX_PSEDetails_Id]...';


GO
CREATE NONCLUSTERED INDEX [IX_PSEDetails_Id]
    ON [dbo].[ImportDetails]([PSEDetails_Id] ASC);


GO
PRINT N'Altering [dbo].[ImportSummaries]...';


GO
ALTER TABLE [dbo].[ImportSummaries] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;

ALTER TABLE [dbo].[ImportSummaries] ALTER COLUMN [ImportDate] DATETIME2 (7) NOT NULL;


GO
PRINT N'Creating [dbo].[ImportSummaries].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[ImportSummaries]([UserId] ASC);


GO
PRINT N'Altering [dbo].[MerchantDetails]...';


GO
ALTER TABLE [dbo].[MerchantDetails] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;

ALTER TABLE [dbo].[MerchantDetails] ALTER COLUMN [PayeeCity] NVARCHAR (40) NULL;

ALTER TABLE [dbo].[MerchantDetails] ALTER COLUMN [PayeeState] NVARCHAR (2) NULL;

ALTER TABLE [dbo].[MerchantDetails] ALTER COLUMN [PayeeZIP] NVARCHAR (9) NULL;


GO
PRINT N'Creating [dbo].[MerchantDetails].[IX_PayerId]...';


GO
CREATE NONCLUSTERED INDEX [IX_PayerId]
    ON [dbo].[MerchantDetails]([PayerId] ASC);


GO
PRINT N'Creating [dbo].[MerchantDetails].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[MerchantDetails]([UserId] ASC);


GO
PRINT N'Altering [dbo].[PayerDetails]...';


GO
ALTER TABLE [dbo].[PayerDetails] ALTER COLUMN [CFSF] NVARCHAR (2) NULL;

ALTER TABLE [dbo].[PayerDetails] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;


GO
PRINT N'Creating [dbo].[PayerDetails].[IX_TransmitterId]...';


GO
CREATE NONCLUSTERED INDEX [IX_TransmitterId]
    ON [dbo].[PayerDetails]([TransmitterId] ASC);


GO
PRINT N'Altering [dbo].[PSEDetails]...';


GO
ALTER TABLE [dbo].[PSEDetails] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[RawTransactions]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_RawTransactions] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [PayeeAccountNumber] NVARCHAR (20)   NULL,
    [TransactionAmount]  DECIMAL (18, 2) NULL,
    [TransactionDate]    DATETIME2 (7)   NOT NULL,
    [TransactionType]    NVARCHAR (3)    NULL,
    [IsActive]           BIT             NOT NULL,
    [DateAdded]          DATETIME2 (7)   NOT NULL,
    [UserId]             BIGINT          NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.RawTransactions1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[RawTransactions])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RawTransactions] ON;
        INSERT INTO [dbo].[tmp_ms_xx_RawTransactions] ([Id], [PayeeAccountNumber], [TransactionAmount], [TransactionDate], [TransactionType], [IsActive], [DateAdded], [UserId])
        SELECT   [Id],
                 [PayeeAccountNumber],
                 [TransactionAmount],
                 [TransactionDate],
                 [TransactionType],
                 [IsActive],
                 [DateAdded],
                 [UserId]
        FROM     [dbo].[RawTransactions]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RawTransactions] OFF;
    END

DROP TABLE [dbo].[RawTransactions];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_RawTransactions]', N'RawTransactions';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.RawTransactions1]', N'PK_dbo.RawTransactions', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[RawTransactions].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[RawTransactions]([UserId] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Status]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Status] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Status1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Status])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Status] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Status] ([Id], [Name])
        SELECT   [Id],
                 [Name]
        FROM     [dbo].[Status]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Status] OFF;
    END

DROP TABLE [dbo].[Status];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Status]', N'Status';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Status1]', N'PK_dbo.Status', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[SubmissionDetails]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_SubmissionDetails] (
    [Id]                       INT             IDENTITY (1, 1) NOT NULL,
    [AccountNo]                NVARCHAR (20)   NULL,
    [SubmissionId]             INT             NOT NULL,
    [SubmissionType]           INT             NOT NULL,
    [PseId]                    INT             NULL,
    [TINType]                  NVARCHAR (1)    NULL,
    [TIN]                      NVARCHAR (9)    NULL,
    [PayerOfficeCode]          NVARCHAR (4)    NULL,
    [GrossAmount]              DECIMAL (18, 2) NULL,
    [CNPTransactionAmount]     DECIMAL (18, 2) NULL,
    [FederalWithHoldingAmount] DECIMAL (18, 2) NULL,
    [JanuaryAmount]            DECIMAL (18, 2) NULL,
    [FebruaryAmount]           DECIMAL (18, 2) NULL,
    [MarchAmount]              DECIMAL (18, 2) NULL,
    [AprilAmount]              DECIMAL (18, 2) NULL,
    [MayAmount]                DECIMAL (18, 2) NULL,
    [JuneAmount]               DECIMAL (18, 2) NULL,
    [JulyAmount]               DECIMAL (18, 2) NULL,
    [AugustAmount]             DECIMAL (18, 2) NULL,
    [SeptemberAmount]          DECIMAL (18, 2) NULL,
    [OctoberAmount]            DECIMAL (18, 2) NULL,
    [NovemberAmount]           DECIMAL (18, 2) NULL,
    [DecemberAmount]           DECIMAL (18, 2) NULL,
    [ForeignCountryIndicator]  NVARCHAR (1)    NULL,
    [FirstPayeeName]           NVARCHAR (40)   NULL,
    [SecondPayeeName]          NVARCHAR (40)   NULL,
    [PayeeMailingAddress]      NVARCHAR (40)   NULL,
    [PayeeCity]                NVARCHAR (40)   NULL,
    [PayeeState]               NVARCHAR (2)    NULL,
    [PayeeZipCode]             NVARCHAR (9)    NULL,
    [SecondTINNoticed]         NVARCHAR (1)    NULL,
    [FillerIndicatorType]      NVARCHAR (1)    NULL,
    [PaymentIndicatorType]     NVARCHAR (1)    NULL,
    [TransactionCount]         INT             NOT NULL,
    [MerchantCategoryCode]     NVARCHAR (4)    NULL,
    [SpecialDataEntry]         NVARCHAR (60)   NULL,
    [StateWithHolding]         DECIMAL (18, 2) NULL,
    [LocalWithHolding]         DECIMAL (18, 2) NULL,
    [CFSF]                     NVARCHAR (2)    NULL,
    [NameControl]              NVARCHAR (4)    NULL,
    [IsActive]                 BIT             NOT NULL,
    [DateAdded]                DATETIME2 (7)   NOT NULL,
    [MerchantId]               INT             NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.SubmissionDetails1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[SubmissionDetails])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SubmissionDetails] ON;
        INSERT INTO [dbo].[tmp_ms_xx_SubmissionDetails] ([Id], [AccountNo], [SubmissionId], [SubmissionType], [TINType], [TIN], [PayerOfficeCode], [GrossAmount], [CNPTransactionAmount], [FederalWithHoldingAmount], [JanuaryAmount], [FebruaryAmount], [MarchAmount], [AprilAmount], [MayAmount], [JuneAmount], [JulyAmount], [AugustAmount], [SeptemberAmount], [OctoberAmount], [NovemberAmount], [DecemberAmount], [ForeignCountryIndicator], [FirstPayeeName], [SecondPayeeName], [PayeeMailingAddress], [PayeeCity], [PayeeState], [PayeeZipCode], [SecondTINNoticed], [FillerIndicatorType], [PaymentIndicatorType], [TransactionCount], [PseId], [MerchantCategoryCode], [SpecialDataEntry], [StateWithHolding], [LocalWithHolding], [CFSF], [IsActive], [DateAdded], [NameControl], [MerchantId])
        SELECT   [Id],
                 [AccountNo],
                 [SubmissionId],
                 [SubmissionType],
                 [TINType],
                 [TIN],
                 [PayerOfficeCode],
                 [GrossAmount],
                 [CNPTransactionAmount],
                 [FederalWithHoldingAmount],
                 [JanuaryAmount],
                 [FebruaryAmount],
                 [MarchAmount],
                 [AprilAmount],
                 [MayAmount],
                 [JuneAmount],
                 [JulyAmount],
                 [AugustAmount],
                 [SeptemberAmount],
                 [OctoberAmount],
                 [NovemberAmount],
                 [DecemberAmount],
                 [ForeignCountryIndicator],
                 [FirstPayeeName],
                 [SecondPayeeName],
                 [PayeeMailingAddress],
                 [PayeeCity],
                 [PayeeState],
                 [PayeeZipCode],
                 [SecondTINNoticed],
                 [FillerIndicatorType],
                 [PaymentIndicatorType],
                 [TransactionCount],
                 [PseId],
                 [MerchantCategoryCode],
                 [SpecialDataEntry],
                 [StateWithHolding],
                 [LocalWithHolding],
                 [CFSF],
                 [IsActive],
                 [DateAdded],
                 [NameControl],
                 [MerchantId]
        FROM     [dbo].[SubmissionDetails]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SubmissionDetails] OFF;
    END

DROP TABLE [dbo].[SubmissionDetails];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_SubmissionDetails]', N'SubmissionDetails';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.SubmissionDetails1]', N'PK_dbo.SubmissionDetails', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[SubmissionDetails].[IX_SubmissionId]...';


GO
CREATE NONCLUSTERED INDEX [IX_SubmissionId]
    ON [dbo].[SubmissionDetails]([SubmissionId] ASC);


GO
PRINT N'Creating [dbo].[SubmissionDetails].[IX_PseId]...';


GO
CREATE NONCLUSTERED INDEX [IX_PseId]
    ON [dbo].[SubmissionDetails]([PseId] ASC);


GO
PRINT N'Creating [dbo].[SubmissionDetails].[IX_MerchantId]...';


GO
CREATE NONCLUSTERED INDEX [IX_MerchantId]
    ON [dbo].[SubmissionDetails]([MerchantId] ASC);


GO
PRINT N'Altering [dbo].[SubmissionStatus]...';


GO
ALTER TABLE [dbo].[SubmissionStatus] ALTER COLUMN [AccountNumber] NVARCHAR (20) NULL;

ALTER TABLE [dbo].[SubmissionStatus] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;

ALTER TABLE [dbo].[SubmissionStatus] ALTER COLUMN [ProcessingDate] DATETIME2 (7) NOT NULL;


GO
PRINT N'Creating [dbo].[SubmissionStatus].[IX_StatusId]...';


GO
CREATE NONCLUSTERED INDEX [IX_StatusId]
    ON [dbo].[SubmissionStatus]([StatusId] ASC);


GO
PRINT N'Altering [dbo].[SubmissionSummaries]...';


GO
ALTER TABLE [dbo].[SubmissionSummaries] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;

ALTER TABLE [dbo].[SubmissionSummaries] ALTER COLUMN [SubmissionDate] DATETIME2 (7) NOT NULL;


GO
PRINT N'Creating [dbo].[SubmissionSummaries].[IX_Status_Id]...';


GO
CREATE NONCLUSTERED INDEX [IX_Status_Id]
    ON [dbo].[SubmissionSummaries]([Status_Id] ASC);


GO
PRINT N'Creating [dbo].[SubmissionSummaries].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[SubmissionSummaries]([UserId] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[TINStatus]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_TINStatus] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.TINStatus1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[TINStatus])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_TINStatus] ON;
        INSERT INTO [dbo].[tmp_ms_xx_TINStatus] ([Id], [Name])
        SELECT   [Id],
                 [Name]
        FROM     [dbo].[TINStatus]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_TINStatus] OFF;
    END

DROP TABLE [dbo].[TINStatus];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_TINStatus]', N'TINStatus';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.TINStatus1]', N'PK_dbo.TINStatus', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[TransmitterDetails]...';


GO
ALTER TABLE [dbo].[TransmitterDetails] ALTER COLUMN [DateAdded] DATETIME2 (7) NOT NULL;


GO
PRINT N'Creating [dbo].[AspNetRoles].[RoleNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserClaims].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserLogins].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserRoles].[IX_RoleId]...';


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserRoles].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUsers].[UserNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);


GO
PRINT N'Creating [dbo].[FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId]...';


GO
ALTER TABLE [dbo].[ImportDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId] FOREIGN KEY ([ImportSummaryId]) REFERENCES [dbo].[ImportSummaries] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId]...';


GO
ALTER TABLE [dbo].[ImportDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId] FOREIGN KEY ([MerchantId]) REFERENCES [dbo].[MerchantDetails] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId]...';


GO
ALTER TABLE [dbo].[ImportDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId] FOREIGN KEY ([SubmissionSummaryId]) REFERENCES [dbo].[SubmissionSummaries] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.ImportDetails_dbo.PSEDetails_PSEDetails_Id]...';


GO
ALTER TABLE [dbo].[ImportDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ImportDetails_dbo.PSEDetails_PSEDetails_Id] FOREIGN KEY ([PSEDetails_Id]) REFERENCES [dbo].[PSEDetails] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.RawTransactions_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[RawTransactions] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.RawTransactions_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.SubmissionStatus_dbo.Status_StatusId]...';


GO
ALTER TABLE [dbo].[SubmissionStatus] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.SubmissionStatus_dbo.Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.SubmissionSummaries_dbo.Status_Status_Id]...';


GO
ALTER TABLE [dbo].[SubmissionSummaries] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.Status_Status_Id] FOREIGN KEY ([Status_Id]) REFERENCES [dbo].[Status] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId] FOREIGN KEY ([MerchantId]) REFERENCES [dbo].[MerchantDetails] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId] FOREIGN KEY ([PseId]) REFERENCES [dbo].[PSEDetails] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId]...';


GO
ALTER TABLE [dbo].[SubmissionDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId] FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[SubmissionSummaries] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Altering [dbo].[PostImportDataProcessing]...';


GO
ALTER PROCEDURE [dbo].[PostImportDataProcessing]
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
		OCTOBER DECIMAL(19,2),
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

	BEGIN TRY  
	BEGIN TRANSACTION K1099

	SET @ProcessLog = '1099K: Import Process Starts' +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Import Date: '+CAST(GETDATE() AS VARCHAR) +CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'File Name: '+@FileName+CHAR(13)+CHAR(10)
	SET @ProcessLog = @ProcessLog + 'Transaction Count: '+CAST(@TotalCount AS VARCHAR) +CHAR(13)+CHAR(10)
	
	-- ARCHIVE EXISTING DATA
	UPDATE [dbo].[RawTransactions] SET Isactive = 0 WHERE IsActive=1
	
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
	
	INSERT INTO @K1099SUMMARYCHART
	SELECT * FROM [dbo].[ImportDataSummary](@PayerId,@YEAR)

	-- ARCHIVE EXISTING SUBMISSION STATUS
	UPDATE S
	SET S.IsActive = 0
	FROM SubmissionStatus S
	INNER JOIN @K1099SUMMARYCHART C ON S.Id = C.SubmissionStatusId AND ISNULL(C.StatusId,0) IN (0,1,2,3,4)
	
	-- CLEAR EXISTING DATA THAT ARE NOT SUBMITTED
	UPDATE D
	SET D.IsActive = 0
	FROM ImportDetails D
	INNER JOIN @K1099SUMMARYCHART C ON D.Id = C.ImportDetailsId AND ISNULL(C.StatusId,0) IN (0,1,2,3,4)

	DECLARE @PAYERNAME VARCHAR(127)
	SELECT @PAYERNAME = p.FirstPayerName FROM [dbo].[PayerDetails] p where Id = @PayerId

	INSERT INTO ImportDetails (AccountNo,ImportSummaryId,TINCheckStatus,TINCheckRemarks,SubmissionSummaryId,TINType,TIN,
	PayerOfficeCode,GrossAmount,CNPTransactionAmount,FederalWithHoldingAmount,
	JanuaryAmount,FebruaryAmount,MarchAmount,AprilAmount,MayAmount,JuneAmount,JulyAmount,AugustAmount,
	SeptemberAmount,OctoberAmount,NovemberAmount,DecemberAmount,ForeignCountryIndicator,FirstPayeeName,
	SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZipCode,SecondTINNoticed,FillerIndicatorType,
	PaymentIndicatorType,TransactionCount,MerchantId,MerchantCategoryCode,SpecialDataEntry,StateWithHolding,
	LocalWithHolding,CFSF,IsActive,DateAdded)

	SELECT C.PayeeAccountNumber,@SummaryId,C.TINCheckStatus,C.TINCheckRemarks,C.SubmissionSummaryId,D.TINType,D.PayeeTIN,
	D.PayeeOfficeCode,C.GrossAmount,C.TotalCPAmount,NULL,
	C.JANUARY,C.FEBRUARY,C.MARCH,C.APRIL,C.MAY,C.JUNE,C.JULY,C.AUGUST,
	C.SEPTEMBER,C.OCTOBER,C.NOVEMBER,C.DECEMBER,NULL,SUBSTRING(D.[PayeeFirstName],1,40), 
	SUBSTRING(D.[PayeeSecondName],1,40),SUBSTRING(D.[PayeeMailingAddress],1,40),SUBSTRING(D.[PayeeCity],1,40),D.[PayeeState],REPLACE(D.[PayeeZIP],'-',''),null,D.[FilerIndicatorType], 
	D.[PaymentIndicatorType],C.TotalTransaction,D.Id,D.[MCC],NULL,NULL,
	NULL,D.CFSF,1,GETDATE()

	FROM @K1099SUMMARYCHART C
	INNER JOIN  [dbo].[MerchantDetails] D ON C.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PaymentYear = @YEAR AND D.PayerId = @PayerId
	WHERE ISNULL(C.StatusId,0) IN (0,1,2,3,4)

	SET @ProcessLog = @ProcessLog + 'Account associated with '+@PAYERNAME+':'+CAST(@@ROWCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)

	----- Correction ------
	INSERT INTO SubmissionStatus (PaymentYear,AccountNumber,ProcessingDate,StatusId,IsActive,DateAdded)
	SELECT @YEAR ,C.PayeeAccountNumber ,GetDate(),4,1,GetDate()
	FROM  @K1099SUMMARYCHART C
	WHERE C.StatusId=3
	--------------------------------------------------------------------

	

	DECLARE @ORPHANT NVARCHAR(1024),@ORPHANTCOUNT INT

	SELECT @ORPHANTCOUNT = COUNT(*)
	FROM @K1099SUMMARYCHART S
	LEFT JOIN  [dbo].[MerchantDetails] D ON S.PayeeAccountNumber = D.PayeeAccountNumber 
		AND D.IsActive = 1 AND D.PaymentYear = @YEAR AND D.PayerId = @PayerId
	WHERE S.TransactionYear = @YEAR AND ISNULL(S.StatusId,0) IN (0,1,2,3,4)


	IF @ORPHANTCOUNT>0
	BEGIN
		SET @ProcessLog = @ProcessLog + 'Account not associated with '+@PAYERNAME+':'+CAST(@ORPHANTCOUNT AS VARCHAR)+CHAR(13)+CHAR(10)
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
	
	

RETURN
GO
PRINT N'Refreshing [dbo].[ImportDataSummary]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ImportDataSummary]';


GO
PRINT N'Checking existing data against newly created constraints';
GO

ALTER TABLE [dbo].[ImportDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.ImportSummaries_ImportSummaryId];

ALTER TABLE [dbo].[ImportDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.MerchantDetails_MerchantId];

ALTER TABLE [dbo].[ImportDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.SubmissionSummaries_SubmissionSummaryId];

ALTER TABLE [dbo].[ImportDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.ImportDetails_dbo.PSEDetails_PSEDetails_Id];

ALTER TABLE [dbo].[RawTransactions] WITH CHECK CHECK CONSTRAINT [FK_dbo.RawTransactions_dbo.AspNetUsers_UserId];

ALTER TABLE [dbo].[SubmissionStatus] WITH CHECK CHECK CONSTRAINT [FK_dbo.SubmissionStatus_dbo.Status_StatusId];

ALTER TABLE [dbo].[SubmissionSummaries] WITH CHECK CHECK CONSTRAINT [FK_dbo.SubmissionSummaries_dbo.Status_Status_Id];

ALTER TABLE [dbo].[SubmissionDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.MerchantDetails_MerchantId];

ALTER TABLE [dbo].[SubmissionDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.PSEDetails_PseId];

ALTER TABLE [dbo].[SubmissionDetails] WITH CHECK CHECK CONSTRAINT [FK_dbo.SubmissionDetails_dbo.SubmissionSummaries_SubmissionId];


GO
PRINT N'Update complete.';


GO
