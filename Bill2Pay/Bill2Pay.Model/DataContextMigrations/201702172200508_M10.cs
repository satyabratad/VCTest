namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M10 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ImportDetails");
            DropPrimaryKey("dbo.SubmissionDetails");
            AlterColumn("dbo.MerchantDetails", "CFSF", c => c.String(maxLength: 2));
            AddPrimaryKey("dbo.ImportDetails", new[] { "IsActive", "AccountNo", "ImportSummaryId" });
            AddPrimaryKey("dbo.SubmissionDetails", new[] { "IsActive", "AccountNo", "SubmissionId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SubmissionDetails");
            DropPrimaryKey("dbo.ImportDetails");
            AlterColumn("dbo.MerchantDetails", "CFSF", c => c.String());
            AddPrimaryKey("dbo.SubmissionDetails", new[] { "AccountNo", "SubmissionId" });
            AddPrimaryKey("dbo.ImportDetails", new[] { "AccountNo", "ImportSummaryId" });
        }
    }
}
