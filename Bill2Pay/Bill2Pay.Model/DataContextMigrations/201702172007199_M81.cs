namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M81 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ImportDetails", "SubmissionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImportDetails", "SubmissionType", c => c.Int(nullable: false));
        }
    }
}
