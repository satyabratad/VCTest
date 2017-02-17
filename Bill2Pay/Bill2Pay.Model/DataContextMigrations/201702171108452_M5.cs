namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PSEMasters", "FillerType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PSEMasters", "FillerType", c => c.Int(nullable: false));
        }
    }
}
