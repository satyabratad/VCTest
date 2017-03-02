namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p31 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ImportDetails", name: "PseId", newName: "PSEDetails_Id");
            RenameIndex(table: "dbo.ImportDetails", name: "IX_PseId", newName: "IX_PSEDetails_Id");
            AddColumn("dbo.ImportDetails", "MerchantId", c => c.Int());
            AddColumn("dbo.SubmissionDetails", "MerchantId", c => c.Int());
            AddColumn("dbo.MerchantDetails", "PaymentYear", c => c.Int(nullable: false));
            AddColumn("dbo.PayerDetails", "PaymentYear", c => c.Int(nullable: false));
            AddColumn("dbo.TransmitterDetails", "PaymentYear", c => c.Int(nullable: false));
            CreateIndex("dbo.ImportDetails", "MerchantId");
            CreateIndex("dbo.SubmissionDetails", "MerchantId");
            AddForeignKey("dbo.ImportDetails", "MerchantId", "dbo.MerchantDetails", "Id");
            AddForeignKey("dbo.SubmissionDetails", "MerchantId", "dbo.MerchantDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmissionDetails", "MerchantId", "dbo.MerchantDetails");
            DropForeignKey("dbo.ImportDetails", "MerchantId", "dbo.MerchantDetails");
            DropIndex("dbo.SubmissionDetails", new[] { "MerchantId" });
            DropIndex("dbo.ImportDetails", new[] { "MerchantId" });
            DropColumn("dbo.TransmitterDetails", "PaymentYear");
            DropColumn("dbo.PayerDetails", "PaymentYear");
            DropColumn("dbo.MerchantDetails", "PaymentYear");
            DropColumn("dbo.SubmissionDetails", "MerchantId");
            DropColumn("dbo.ImportDetails", "MerchantId");
            RenameIndex(table: "dbo.ImportDetails", name: "IX_PSEDetails_Id", newName: "IX_PseId");
            RenameColumn(table: "dbo.ImportDetails", name: "PSEDetails_Id", newName: "PseId");
        }
    }
}
