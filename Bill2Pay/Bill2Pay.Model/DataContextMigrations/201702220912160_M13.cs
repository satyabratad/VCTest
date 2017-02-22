namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmissionStatus", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionStatus", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmissionStatus", "DateAdded");
            DropColumn("dbo.SubmissionStatus", "IsActive");
        }
    }
}
