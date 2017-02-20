namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M11 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ImportDetails");
            DropPrimaryKey("dbo.SubmissionDetails");
            AddColumn("dbo.ImportDetails", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SubmissionDetails", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ImportDetails", "AccountNo", c => c.String());
            AlterColumn("dbo.SubmissionDetails", "AccountNo", c => c.String());
            AddPrimaryKey("dbo.ImportDetails", "Id");
            AddPrimaryKey("dbo.SubmissionDetails", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SubmissionDetails");
            DropPrimaryKey("dbo.ImportDetails");
            AlterColumn("dbo.SubmissionDetails", "AccountNo", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ImportDetails", "AccountNo", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.SubmissionDetails", "Id");
            DropColumn("dbo.ImportDetails", "Id");
            AddPrimaryKey("dbo.SubmissionDetails", new[] { "IsActive", "AccountNo", "SubmissionId" });
            AddPrimaryKey("dbo.ImportDetails", new[] { "IsActive", "AccountNo", "ImportSummaryId" });
        }
    }
}
