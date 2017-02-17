namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ImportDetails", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.PSEMasters", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.PSEMasters", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubmissionDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubmissionDetails", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmissionDetails", "DateAdded");
            DropColumn("dbo.SubmissionDetails", "IsActive");
            DropColumn("dbo.PSEMasters", "DateAdded");
            DropColumn("dbo.PSEMasters", "IsActive");
            DropColumn("dbo.ImportDetails", "DateAdded");
            DropColumn("dbo.ImportDetails", "IsActive");
        }
    }
}
