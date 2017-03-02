namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class P1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(),
                        ImportSummaryId = c.Int(nullable: false),
                        TINCheckStatus = c.String(maxLength: 2),
                        TINCheckRemarks = c.String(maxLength: 512),
                        SubmissionSummaryId = c.Int(),
                        TINType = c.String(maxLength: 1),
                        TIN = c.String(maxLength: 9),
                        PayerOfficeCode = c.String(maxLength: 4),
                        GrossAmount = c.Decimal(precision: 18, scale: 2),
                        CNPTransactionAmount = c.Decimal(precision: 18, scale: 2),
                        FederalWithHoldingAmount = c.Decimal(precision: 18, scale: 2),
                        JanuaryAmount = c.Decimal(precision: 18, scale: 2),
                        FebruaryAmount = c.Decimal(precision: 18, scale: 2),
                        MarchAmount = c.Decimal(precision: 18, scale: 2),
                        AprilAmount = c.Decimal(precision: 18, scale: 2),
                        MayAmount = c.Decimal(precision: 18, scale: 2),
                        JuneAmount = c.Decimal(precision: 18, scale: 2),
                        JulyAmount = c.Decimal(precision: 18, scale: 2),
                        AugustAmount = c.Decimal(precision: 18, scale: 2),
                        SeptemberAmount = c.Decimal(precision: 18, scale: 2),
                        OctoberAmount = c.Decimal(precision: 18, scale: 2),
                        NovemberAmount = c.Decimal(precision: 18, scale: 2),
                        DecemberAmount = c.Decimal(precision: 18, scale: 2),
                        ForeignCountryIndicator = c.String(maxLength: 1),
                        FirstPayeeName = c.String(maxLength: 40),
                        SecondPayeeName = c.String(maxLength: 40),
                        PayeeMailingAddress = c.String(maxLength: 40),
                        PayeeCity = c.String(maxLength: 40),
                        PayeeState = c.String(maxLength: 2),
                        PayeeZipCode = c.String(maxLength: 9),
                        SecondTINNoticed = c.String(maxLength: 1),
                        FillerIndicatorType = c.String(maxLength: 1),
                        PaymentIndicatorType = c.String(maxLength: 1),
                        TransactionCount = c.Int(nullable: false),
                        PseId = c.Int(),
                        MerchantCategoryCode = c.String(maxLength: 4),
                        SpecialDataEntry = c.String(maxLength: 60),
                        StateWithHolding = c.Decimal(precision: 18, scale: 2),
                        LocalWithHolding = c.Decimal(precision: 18, scale: 2),
                        CFSF = c.String(maxLength: 2),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportSummaries", t => t.ImportSummaryId, cascadeDelete: true)
                .ForeignKey("dbo.PSEDetails", t => t.PseId)
                .ForeignKey("dbo.SubmissionSummaries", t => t.SubmissionSummaryId)
                .Index(t => t.ImportSummaryId)
                .Index(t => t.SubmissionSummaryId)
                .Index(t => t.PseId);
            
            CreateTable(
                "dbo.ImportSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentYear = c.Int(nullable: false),
                        ImportDate = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 100),
                        RecordCount = c.Int(),
                        ProcessLog = c.String(maxLength: 1024),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsDefaultPasswordChanged = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PSEDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransmitterTIN = c.String(maxLength: 9),
                        TransmitterControlCode = c.String(maxLength: 5),
                        TestFileIndicator = c.String(maxLength: 1),
                        TransmitterForeignEntityIndicator = c.String(maxLength: 1),
                        TransmitterName = c.String(maxLength: 40),
                        TransmitterNameContinued = c.String(maxLength: 40),
                        CompanyName = c.String(maxLength: 40),
                        CompanyNameContinued = c.String(maxLength: 40),
                        CompanyMailingAddress = c.String(maxLength: 40),
                        CompanyCity = c.String(maxLength: 40),
                        CompanyState = c.String(maxLength: 2),
                        CompanyZIP = c.String(maxLength: 9),
                        TotalNumberofPayees = c.Int(nullable: false),
                        ContactName = c.String(maxLength: 40),
                        ContactTelephoneNumber = c.String(maxLength: 15),
                        ContactEmailAddress = c.String(maxLength: 50),
                        VendorIndicator = c.String(maxLength: 1),
                        VendorName = c.String(maxLength: 40),
                        VendorMailingAddress = c.String(maxLength: 40),
                        VendorCity = c.String(maxLength: 40),
                        VendorState = c.String(maxLength: 2),
                        VendorZIP = c.String(maxLength: 9),
                        VendorContactName = c.String(maxLength: 40),
                        VendorContactTelephoneNumber = c.String(maxLength: 15),
                        VendorForeignEntityIndicator = c.String(maxLength: 1),
                        CFSF = c.String(maxLength: 1),
                        PayerTIN = c.String(maxLength: 9),
                        PayerNameControl = c.String(maxLength: 4),
                        LastFilingIndicator = c.String(maxLength: 1),
                        ReturnType = c.String(maxLength: 2),
                        AmountCodes = c.String(maxLength: 16),
                        PayerForeignEntityIndicator = c.String(maxLength: 1),
                        FirstPayerName = c.String(maxLength: 40),
                        SecondPayerName = c.String(maxLength: 40),
                        TransferAgentIndicator = c.String(maxLength: 1),
                        PayerShippingAddress = c.String(maxLength: 40),
                        PayerCity = c.String(maxLength: 40),
                        PayerState = c.String(maxLength: 2),
                        PayerZIP = c.String(maxLength: 9),
                        PayerTelephoneNumber = c.String(maxLength: 15),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubmissionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(),
                        SubmissionId = c.Int(nullable: false),
                        SubmissionType = c.Int(nullable: false),
                        TINType = c.String(maxLength: 1),
                        TIN = c.String(maxLength: 9),
                        PayerOfficeCode = c.String(maxLength: 4),
                        GrossAmount = c.Decimal(precision: 18, scale: 2),
                        CNPTransactionAmount = c.Decimal(precision: 18, scale: 2),
                        FederalWithHoldingAmount = c.Decimal(precision: 18, scale: 2),
                        JanuaryAmount = c.Decimal(precision: 18, scale: 2),
                        FebruaryAmount = c.Decimal(precision: 18, scale: 2),
                        MarchAmount = c.Decimal(precision: 18, scale: 2),
                        AprilAmount = c.Decimal(precision: 18, scale: 2),
                        MayAmount = c.Decimal(precision: 18, scale: 2),
                        JuneAmount = c.Decimal(precision: 18, scale: 2),
                        JulyAmount = c.Decimal(precision: 18, scale: 2),
                        AugustAmount = c.Decimal(precision: 18, scale: 2),
                        SeptemberAmount = c.Decimal(precision: 18, scale: 2),
                        OctoberAmount = c.Decimal(precision: 18, scale: 2),
                        NovemberAmount = c.Decimal(precision: 18, scale: 2),
                        DecemberAmount = c.Decimal(precision: 18, scale: 2),
                        ForeignCountryIndicator = c.String(maxLength: 1),
                        FirstPayeeName = c.String(maxLength: 40),
                        SecondPayeeName = c.String(maxLength: 40),
                        PayeeMailingAddress = c.String(maxLength: 40),
                        PayeeCity = c.String(maxLength: 40),
                        PayeeState = c.String(maxLength: 2),
                        PayeeZipCode = c.String(maxLength: 9),
                        SecondTINNoticed = c.String(maxLength: 1),
                        FillerIndicatorType = c.String(maxLength: 1),
                        PaymentIndicatorType = c.String(maxLength: 1),
                        TransactionCount = c.Int(nullable: false),
                        PseId = c.Int(),
                        MerchantCategoryCode = c.String(maxLength: 4),
                        SpecialDataEntry = c.String(maxLength: 60),
                        StateWithHolding = c.Decimal(precision: 18, scale: 2),
                        LocalWithHolding = c.Decimal(precision: 18, scale: 2),
                        CFSF = c.String(maxLength: 2),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PSEDetails", t => t.PseId)
                .ForeignKey("dbo.SubmissionSummaries", t => t.SubmissionId, cascadeDelete: true)
                .Index(t => t.SubmissionId)
                .Index(t => t.PseId);
            
            CreateTable(
                "dbo.SubmissionSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentYear = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.UserId)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.MerchantDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayeeAccountNumber = c.String(maxLength: 255),
                        TINType = c.String(maxLength: 255),
                        PayeeTIN = c.String(maxLength: 255),
                        PayeeOfficeCode = c.String(maxLength: 255),
                        PayeeFirstName = c.String(maxLength: 255),
                        PayeeSecondName = c.String(maxLength: 255),
                        PayeeMailingAddress = c.String(maxLength: 255),
                        PayeeCity = c.String(maxLength: 255),
                        PayeeState = c.String(maxLength: 255),
                        PayeeZIP = c.String(maxLength: 255),
                        FilerIndicatorType = c.String(maxLength: 255),
                        PaymentIndicatorType = c.String(maxLength: 255),
                        MCC = c.String(maxLength: 255),
                        CFSF = c.String(maxLength: 2),
                        PayerId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.PayerDetails", t => t.PayerId, cascadeDelete: true)
                .Index(t => t.PayerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PayerDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CFSF = c.String(maxLength: 1),
                        PayerTIN = c.String(maxLength: 9),
                        PayerNameControl = c.String(maxLength: 4),
                        LastFilingIndicator = c.String(maxLength: 1),
                        ReturnType = c.String(maxLength: 2),
                        AmountCodes = c.String(maxLength: 16),
                        PayerForeignEntityIndicator = c.String(maxLength: 1),
                        FirstPayerName = c.String(maxLength: 40),
                        SecondPayerName = c.String(maxLength: 40),
                        TransferAgentIndicator = c.String(maxLength: 1),
                        PayerShippingAddress = c.String(maxLength: 40),
                        PayerCity = c.String(maxLength: 40),
                        PayerState = c.String(maxLength: 2),
                        PayerZIP = c.String(maxLength: 9),
                        PayerTelephoneNumber = c.String(maxLength: 15),
                        TransmitterId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransmitterDetails", t => t.TransmitterId, cascadeDelete: true)
                .Index(t => t.TransmitterId);
            
            CreateTable(
                "dbo.TransmitterDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransmitterTIN = c.String(maxLength: 9),
                        TransmitterControlCode = c.String(maxLength: 5),
                        TestFileIndicator = c.String(maxLength: 1),
                        TransmitterForeignEntityIndicator = c.String(maxLength: 1),
                        TransmitterName = c.String(maxLength: 40),
                        TransmitterNameContinued = c.String(maxLength: 40),
                        CompanyName = c.String(maxLength: 40),
                        CompanyNameContinued = c.String(maxLength: 40),
                        CompanyMailingAddress = c.String(maxLength: 40),
                        CompanyCity = c.String(maxLength: 40),
                        CompanyState = c.String(maxLength: 2),
                        CompanyZIP = c.String(maxLength: 9),
                        TotalNumberofPayees = c.Int(nullable: false),
                        ContactName = c.String(maxLength: 40),
                        ContactTelephoneNumber = c.String(maxLength: 15),
                        ContactEmailAddress = c.String(maxLength: 50),
                        VendorIndicator = c.String(maxLength: 1),
                        VendorName = c.String(maxLength: 40),
                        VendorMailingAddress = c.String(maxLength: 40),
                        VendorCity = c.String(maxLength: 40),
                        VendorState = c.String(maxLength: 2),
                        VendorZIP = c.String(maxLength: 9),
                        VendorContactName = c.String(maxLength: 40),
                        VendorContactTelephoneNumber = c.String(maxLength: 15),
                        VendorForeignEntityIndicator = c.String(maxLength: 1),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RawTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayeeAccountNumber = c.String(maxLength: 256),
                        TransactionAmount = c.Decimal(precision: 18, scale: 2),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RawTransactionStagings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TINType = c.String(maxLength: 255),
                        PayeeTIN = c.String(maxLength: 255),
                        PayeeAccountNumber = c.String(maxLength: 255),
                        PayeeOfficeCode = c.String(maxLength: 255),
                        CardPresentTransactions = c.String(maxLength: 255),
                        FederalIncomeTaxWithheld = c.String(maxLength: 255),
                        StateIncomeTaxWithheld = c.String(maxLength: 255),
                        TransactionAmount = c.String(maxLength: 255),
                        TransactionDate = c.String(maxLength: 255),
                        TransactionType = c.String(maxLength: 255),
                        PayeeFirstName = c.String(maxLength: 255),
                        PayeeSecondName = c.String(maxLength: 255),
                        PayeeMailingAddress = c.String(maxLength: 255),
                        PayeeCity = c.String(maxLength: 255),
                        PayeeState = c.String(maxLength: 255),
                        PayeeZIP = c.String(maxLength: 255),
                        FilerIndicatorType = c.String(maxLength: 255),
                        PaymentIndicatorType = c.String(maxLength: 255),
                        MCC = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubmissionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentYear = c.Int(nullable: false),
                        AccountNumber = c.String(maxLength: 255),
                        ProcessingDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.TINStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmissionStatus", "StatusId", "dbo.Status");
            DropForeignKey("dbo.SubmissionSummaries", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RawTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MerchantDetails", "PayerId", "dbo.PayerDetails");
            DropForeignKey("dbo.PayerDetails", "TransmitterId", "dbo.TransmitterDetails");
            DropForeignKey("dbo.MerchantDetails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ImportDetails", "SubmissionSummaryId", "dbo.SubmissionSummaries");
            DropForeignKey("dbo.ImportDetails", "PseId", "dbo.PSEDetails");
            DropForeignKey("dbo.SubmissionDetails", "SubmissionId", "dbo.SubmissionSummaries");
            DropForeignKey("dbo.SubmissionSummaries", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubmissionDetails", "PseId", "dbo.PSEDetails");
            DropForeignKey("dbo.ImportDetails", "ImportSummaryId", "dbo.ImportSummaries");
            DropForeignKey("dbo.ImportSummaries", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SubmissionStatus", new[] { "StatusId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RawTransactions", new[] { "UserId" });
            DropIndex("dbo.PayerDetails", new[] { "TransmitterId" });
            DropIndex("dbo.MerchantDetails", new[] { "UserId" });
            DropIndex("dbo.MerchantDetails", new[] { "PayerId" });
            DropIndex("dbo.SubmissionSummaries", new[] { "Status_Id" });
            DropIndex("dbo.SubmissionSummaries", new[] { "UserId" });
            DropIndex("dbo.SubmissionDetails", new[] { "PseId" });
            DropIndex("dbo.SubmissionDetails", new[] { "SubmissionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ImportSummaries", new[] { "UserId" });
            DropIndex("dbo.ImportDetails", new[] { "PseId" });
            DropIndex("dbo.ImportDetails", new[] { "SubmissionSummaryId" });
            DropIndex("dbo.ImportDetails", new[] { "ImportSummaryId" });
            DropTable("dbo.TINStatus");
            DropTable("dbo.SubmissionStatus");
            DropTable("dbo.Status");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RawTransactionStagings");
            DropTable("dbo.RawTransactions");
            DropTable("dbo.TransmitterDetails");
            DropTable("dbo.PayerDetails");
            DropTable("dbo.MerchantDetails");
            DropTable("dbo.SubmissionSummaries");
            DropTable("dbo.SubmissionDetails");
            DropTable("dbo.PSEDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ImportSummaries");
            DropTable("dbo.ImportDetails");
        }
    }
}
