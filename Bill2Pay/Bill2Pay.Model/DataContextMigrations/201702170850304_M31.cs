namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportDetails", "SubmissionType", c => c.Int(nullable: false));
            AddColumn("dbo.ImportDetails", "FillerType", c => c.Int(nullable: false));
            AddColumn("dbo.SubmissionDetails", "SubmissionType", c => c.Int(nullable: false));
            AddColumn("dbo.SubmissionDetails", "FillerType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmissionDetails", "FillerType");
            DropColumn("dbo.SubmissionDetails", "SubmissionType");
            DropColumn("dbo.ImportDetails", "FillerType");
            DropColumn("dbo.ImportDetails", "SubmissionType");
        }
    }
}
