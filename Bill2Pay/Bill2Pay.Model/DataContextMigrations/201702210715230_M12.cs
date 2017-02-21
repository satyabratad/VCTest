namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsDefaultPasswordChanged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsDefaultPasswordChanged");
        }
    }
}
