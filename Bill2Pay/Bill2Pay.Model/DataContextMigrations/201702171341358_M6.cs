namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M6 : DbMigration
    {
        public override void Up()
        {
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
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            AddColumn("dbo.ImportSummaries", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ImportSummaries", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubmissionSummaries", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionSummaries", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RawTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MerchantDetails", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RawTransactions", new[] { "UserId" });
            DropIndex("dbo.MerchantDetails", new[] { "UserId" });
            DropColumn("dbo.SubmissionSummaries", "DateAdded");
            DropColumn("dbo.SubmissionSummaries", "IsActive");
            DropColumn("dbo.ImportSummaries", "DateAdded");
            DropColumn("dbo.ImportSummaries", "IsActive");
            DropTable("dbo.RawTransactions");
            DropTable("dbo.MerchantDetails");
        }
    }
}
