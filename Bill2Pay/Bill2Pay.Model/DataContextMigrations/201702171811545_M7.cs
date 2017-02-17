namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportSummaries", "ProcessLog", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImportSummaries", "ProcessLog");
        }
    }
}
