namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M14 : DbMigration
    {
        public override void Up()
        {
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
                        IsActive = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransmitterDetails");
            DropTable("dbo.PayerDetails");
        }
    }
}
