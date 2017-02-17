namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PSEMasters", "TransmitterTIN", c => c.String(maxLength: 9));
            AddColumn("dbo.PSEMasters", "TransmitterControlCode", c => c.String(maxLength: 5));
            AddColumn("dbo.PSEMasters", "TestFileIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "TransmitterForeignEntityIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "TransmitterName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "TransmitterNameContinued", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "CompanyName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "CompanyNameContinued", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "CompanyMailingAddress", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "CompanyCity", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "CompanyState", c => c.String(maxLength: 2));
            AddColumn("dbo.PSEMasters", "CompanyZIP", c => c.String(maxLength: 9));
            AddColumn("dbo.PSEMasters", "TotalNumberofPayees", c => c.Int(nullable: false));
            AddColumn("dbo.PSEMasters", "ContactName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "ContactTelephoneNumber", c => c.String(maxLength: 15));
            AddColumn("dbo.PSEMasters", "ContactEmailAddress", c => c.String(maxLength: 50));
            AddColumn("dbo.PSEMasters", "VendorIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "VendorName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "VendorMailingAddress", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "VendorCity", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "VendorState", c => c.String(maxLength: 2));
            AddColumn("dbo.PSEMasters", "VendorZIP", c => c.String(maxLength: 9));
            AddColumn("dbo.PSEMasters", "VendorContactName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "VendorContactTelephoneNumber", c => c.String(maxLength: 15));
            AddColumn("dbo.PSEMasters", "VendorForeignEntityIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "CFSF", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "PayerTIN", c => c.String(maxLength: 9));
            AddColumn("dbo.PSEMasters", "PayerNameControl", c => c.String(maxLength: 4));
            AddColumn("dbo.PSEMasters", "LastFilingIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "ReturnType", c => c.String(maxLength: 2));
            AddColumn("dbo.PSEMasters", "AmountCodes", c => c.String(maxLength: 16));
            AddColumn("dbo.PSEMasters", "PayerForeignEntityIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "FirstPayerName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "SecondPayerName", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "TransferAgentIndicator", c => c.String(maxLength: 1));
            AddColumn("dbo.PSEMasters", "PayerShippingAddress", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "PayerCity", c => c.String(maxLength: 40));
            AddColumn("dbo.PSEMasters", "PayerState", c => c.String(maxLength: 2));
            AddColumn("dbo.PSEMasters", "PayerZIP", c => c.String(maxLength: 9));
            AddColumn("dbo.PSEMasters", "PayerTelephoneNumber", c => c.String(maxLength: 15));
            DropColumn("dbo.PSEMasters", "Name");
            DropColumn("dbo.PSEMasters", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PSEMasters", "Address", c => c.String(maxLength: 512));
            AddColumn("dbo.PSEMasters", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.PSEMasters", "PayerTelephoneNumber");
            DropColumn("dbo.PSEMasters", "PayerZIP");
            DropColumn("dbo.PSEMasters", "PayerState");
            DropColumn("dbo.PSEMasters", "PayerCity");
            DropColumn("dbo.PSEMasters", "PayerShippingAddress");
            DropColumn("dbo.PSEMasters", "TransferAgentIndicator");
            DropColumn("dbo.PSEMasters", "SecondPayerName");
            DropColumn("dbo.PSEMasters", "FirstPayerName");
            DropColumn("dbo.PSEMasters", "PayerForeignEntityIndicator");
            DropColumn("dbo.PSEMasters", "AmountCodes");
            DropColumn("dbo.PSEMasters", "ReturnType");
            DropColumn("dbo.PSEMasters", "LastFilingIndicator");
            DropColumn("dbo.PSEMasters", "PayerNameControl");
            DropColumn("dbo.PSEMasters", "PayerTIN");
            DropColumn("dbo.PSEMasters", "CFSF");
            DropColumn("dbo.PSEMasters", "VendorForeignEntityIndicator");
            DropColumn("dbo.PSEMasters", "VendorContactTelephoneNumber");
            DropColumn("dbo.PSEMasters", "VendorContactName");
            DropColumn("dbo.PSEMasters", "VendorZIP");
            DropColumn("dbo.PSEMasters", "VendorState");
            DropColumn("dbo.PSEMasters", "VendorCity");
            DropColumn("dbo.PSEMasters", "VendorMailingAddress");
            DropColumn("dbo.PSEMasters", "VendorName");
            DropColumn("dbo.PSEMasters", "VendorIndicator");
            DropColumn("dbo.PSEMasters", "ContactEmailAddress");
            DropColumn("dbo.PSEMasters", "ContactTelephoneNumber");
            DropColumn("dbo.PSEMasters", "ContactName");
            DropColumn("dbo.PSEMasters", "TotalNumberofPayees");
            DropColumn("dbo.PSEMasters", "CompanyZIP");
            DropColumn("dbo.PSEMasters", "CompanyState");
            DropColumn("dbo.PSEMasters", "CompanyCity");
            DropColumn("dbo.PSEMasters", "CompanyMailingAddress");
            DropColumn("dbo.PSEMasters", "CompanyNameContinued");
            DropColumn("dbo.PSEMasters", "CompanyName");
            DropColumn("dbo.PSEMasters", "TransmitterNameContinued");
            DropColumn("dbo.PSEMasters", "TransmitterName");
            DropColumn("dbo.PSEMasters", "TransmitterForeignEntityIndicator");
            DropColumn("dbo.PSEMasters", "TestFileIndicator");
            DropColumn("dbo.PSEMasters", "TransmitterControlCode");
            DropColumn("dbo.PSEMasters", "TransmitterTIN");
        }
    }
}
