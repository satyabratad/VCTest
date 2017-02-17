namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PSEMasters", "SubmissionType", c => c.Int(nullable: false));
            AddColumn("dbo.PSEMasters", "FillerType", c => c.Int(nullable: false));
            DropColumn("dbo.ImportDetails", "SubmissionType");
            DropColumn("dbo.ImportDetails", "FillerType");
            DropColumn("dbo.SubmissionDetails", "SubmissionType");
            DropColumn("dbo.SubmissionDetails", "FillerType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubmissionDetails", "FillerType", c => c.Int(nullable: false));
            AddColumn("dbo.SubmissionDetails", "SubmissionType", c => c.Int(nullable: false));
            AddColumn("dbo.ImportDetails", "FillerType", c => c.Int(nullable: false));
            AddColumn("dbo.ImportDetails", "SubmissionType", c => c.Int(nullable: false));
            DropColumn("dbo.PSEMasters", "FillerType");
            DropColumn("dbo.PSEMasters", "SubmissionType");
        }
    }
}
