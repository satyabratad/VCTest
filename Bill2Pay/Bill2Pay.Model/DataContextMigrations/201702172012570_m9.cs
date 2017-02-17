namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MerchantDetails", "CFSF", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MerchantDetails", "CFSF");
        }
    }
}
