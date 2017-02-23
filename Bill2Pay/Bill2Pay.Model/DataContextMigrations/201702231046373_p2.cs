namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RawTransactions", "TransactionType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RawTransactions", "TransactionType", c => c.Int(nullable: false));
        }
    }
}
