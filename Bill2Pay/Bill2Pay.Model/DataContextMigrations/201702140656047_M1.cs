namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportDetails",
                c => new
                    {
                        AccountNo = c.String(nullable: false, maxLength: 128),
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
                        PSEMasterId = c.Int(nullable: false),
                        MerchantCategoryCode = c.String(maxLength: 4),
                        SpecialDataEntry = c.String(maxLength: 60),
                        StateWithHolding = c.Decimal(precision: 18, scale: 2),
                        LocalWithHolding = c.Decimal(precision: 18, scale: 2),
                        CFSF = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => new { t.AccountNo, t.ImportSummaryId })
                .ForeignKey("dbo.ImportSummaries", t => t.ImportSummaryId, cascadeDelete: true)
                .ForeignKey("dbo.PSEMasters", t => t.PSEMasterId, cascadeDelete: true)
                .ForeignKey("dbo.SubmissionSummaries", t => t.SubmissionSummaryId)
                .Index(t => t.ImportSummaryId)
                .Index(t => t.SubmissionSummaryId)
                .Index(t => t.PSEMasterId);
            
            CreateTable(
                "dbo.ImportSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentYear = c.Int(nullable: false),
                        ImportDate = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 100),
                        RecordCount = c.Int(),
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
                "dbo.PSEMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Address = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubmissionDetails",
                c => new
                    {
                        AccountNo = c.String(nullable: false, maxLength: 128),
                        SubmissionId = c.Int(nullable: false),
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
                        PSEMasterId = c.Int(nullable: false),
                        MerchantCategoryCode = c.String(maxLength: 4),
                        SpecialDataEntry = c.String(maxLength: 60),
                        StateWithHolding = c.Decimal(precision: 18, scale: 2),
                        LocalWithHolding = c.Decimal(precision: 18, scale: 2),
                        CFSF = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => new { t.AccountNo, t.SubmissionId })
                .ForeignKey("dbo.PSEMasters", t => t.PSEMasterId, cascadeDelete: true)
                .ForeignKey("dbo.SubmissionSummaries", t => t.SubmissionId, cascadeDelete: true)
                .Index(t => t.SubmissionId)
                .Index(t => t.PSEMasterId);
            
            CreateTable(
                "dbo.SubmissionSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentYear = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.UserId)
                .Index(t => t.Status_Id);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmissionStatus", "StatusId", "dbo.Status");
            DropForeignKey("dbo.SubmissionSummaries", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ImportDetails", "SubmissionSummaryId", "dbo.SubmissionSummaries");
            DropForeignKey("dbo.ImportDetails", "PSEMasterId", "dbo.PSEMasters");
            DropForeignKey("dbo.SubmissionDetails", "SubmissionId", "dbo.SubmissionSummaries");
            DropForeignKey("dbo.SubmissionSummaries", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubmissionDetails", "PSEMasterId", "dbo.PSEMasters");
            DropForeignKey("dbo.ImportDetails", "ImportSummaryId", "dbo.ImportSummaries");
            DropForeignKey("dbo.ImportSummaries", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SubmissionStatus", new[] { "StatusId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubmissionSummaries", new[] { "Status_Id" });
            DropIndex("dbo.SubmissionSummaries", new[] { "UserId" });
            DropIndex("dbo.SubmissionDetails", new[] { "PSEMasterId" });
            DropIndex("dbo.SubmissionDetails", new[] { "SubmissionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ImportSummaries", new[] { "UserId" });
            DropIndex("dbo.ImportDetails", new[] { "PSEMasterId" });
            DropIndex("dbo.ImportDetails", new[] { "SubmissionSummaryId" });
            DropIndex("dbo.ImportDetails", new[] { "ImportSummaryId" });
            DropTable("dbo.SubmissionStatus");
            DropTable("dbo.Status");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RawTransactionStagings");
            DropTable("dbo.SubmissionSummaries");
            DropTable("dbo.SubmissionDetails");
            DropTable("dbo.PSEMasters");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ImportSummaries");
            DropTable("dbo.ImportDetails");
        }
    }
}
