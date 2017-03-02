namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportDetails", "NameControl", c => c.String(maxLength: 4));
            AddColumn("dbo.SubmissionDetails", "NameControl", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmissionDetails", "NameControl");
            DropColumn("dbo.ImportDetails", "NameControl");
        }
    }
}
