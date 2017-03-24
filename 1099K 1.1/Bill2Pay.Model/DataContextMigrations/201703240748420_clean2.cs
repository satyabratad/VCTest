namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clean2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RawTransactionStagings", "TransactionType", c => c.String(maxLength: 3));
            DropColumn("dbo.MerchantDetails", "PaymentYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MerchantDetails", "PaymentYear", c => c.Int(nullable: false));
            AlterColumn("dbo.RawTransactionStagings", "TransactionType", c => c.String(maxLength: 1));
        }
    }
}
