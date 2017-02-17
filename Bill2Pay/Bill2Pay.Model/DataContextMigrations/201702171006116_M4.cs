namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportDetails", "SubmissionType", c => c.Int(nullable: false));
            AddColumn("dbo.SubmissionDetails", "SubmissionType", c => c.Int(nullable: false));
            DropColumn("dbo.PSEMasters", "SubmissionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PSEMasters", "SubmissionType", c => c.Int(nullable: false));
            DropColumn("dbo.SubmissionDetails", "SubmissionType");
            DropColumn("dbo.ImportDetails", "SubmissionType");
        }
    }
}
