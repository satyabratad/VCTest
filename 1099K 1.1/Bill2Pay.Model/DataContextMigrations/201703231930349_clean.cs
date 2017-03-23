namespace Bill2Pay.Model.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class clean : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ImportDetails", "AccountNo", "AccountNumber");
            RenameColumn("dbo.MerchantDetails", "PayeeFirstName", "FirstPayeeName");
            RenameColumn("dbo.MerchantDetails", "PayeeSecondName", "SecondPayeeName");
            RenameColumn("dbo.SubmissionDetails", "AccountNo", "AccountNumber");

            AlterColumn("dbo.ImportDetails", "AccountNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.MerchantDetails", "FirstPayeeName", c => c.String(maxLength: 40));
            AlterColumn("dbo.MerchantDetails", "SecondPayeeName", c => c.String(maxLength: 40));
            AlterColumn("dbo.SubmissionDetails", "AccountNumber", c => c.String(maxLength: 20));

            AlterColumn("dbo.ImportDetails", "CFSF", c => c.String(maxLength: 1));
            AlterColumn("dbo.MerchantDetails", "PayeeAccountNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.MerchantDetails", "TINType", c => c.String(maxLength: 1));
            AlterColumn("dbo.MerchantDetails", "PayeeTIN", c => c.String(maxLength: 9));
            AlterColumn("dbo.MerchantDetails", "PayeeOfficeCode", c => c.String(maxLength: 4));
            AlterColumn("dbo.MerchantDetails", "PayeeMailingAddress", c => c.String(maxLength: 40));
            AlterColumn("dbo.MerchantDetails", "FilerIndicatorType", c => c.String(maxLength: 1));
            AlterColumn("dbo.MerchantDetails", "PaymentIndicatorType", c => c.String(maxLength: 1));
            AlterColumn("dbo.MerchantDetails", "MCC", c => c.String(maxLength: 4));
            AlterColumn("dbo.MerchantDetails", "CFSF", c => c.String(maxLength: 1));
            AlterColumn("dbo.PayerDetails", "CFSF", c => c.String(maxLength: 1));
            AlterColumn("dbo.SubmissionDetails", "CFSF", c => c.String(maxLength: 1));
            AlterColumn("dbo.RawTransactionStagings", "PayeeAccountNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.RawTransactionStagings", "TransactionAmount", c => c.String(maxLength: 20));
            AlterColumn("dbo.RawTransactionStagings", "TransactionDate", c => c.String(maxLength: 32));
            AlterColumn("dbo.RawTransactionStagings", "TransactionType", c => c.String(maxLength: 1));

            

            DropColumn("dbo.RawTransactionStagings", "TINType");
            DropColumn("dbo.RawTransactionStagings", "PayeeTIN");
            DropColumn("dbo.RawTransactionStagings", "PayeeOfficeCode");
            DropColumn("dbo.RawTransactionStagings", "CardPresentTransactions");
            DropColumn("dbo.RawTransactionStagings", "FederalIncomeTaxWithheld");
            DropColumn("dbo.RawTransactionStagings", "StateIncomeTaxWithheld");
            DropColumn("dbo.RawTransactionStagings", "PayeeFirstName");
            DropColumn("dbo.RawTransactionStagings", "PayeeSecondName");
            DropColumn("dbo.RawTransactionStagings", "PayeeMailingAddress");
            DropColumn("dbo.RawTransactionStagings", "PayeeCity");
            DropColumn("dbo.RawTransactionStagings", "PayeeState");
            DropColumn("dbo.RawTransactionStagings", "PayeeZIP");
            DropColumn("dbo.RawTransactionStagings", "FilerIndicatorType");
            DropColumn("dbo.RawTransactionStagings", "PaymentIndicatorType");
            DropColumn("dbo.RawTransactionStagings", "MCC");
        }

        public override void Down()
        {
            RenameColumn("dbo.ImportDetails", "AccountNumber", "AccountNo");
            RenameColumn("dbo.MerchantDetails", "FirstPayeeName", "PayeeFirstName");
            RenameColumn("dbo.MerchantDetails", "SecondPayeeName", "PayeeSecondName");
            RenameColumn("dbo.SubmissionDetails", "AccountNumber", "AccountNo");

            AddColumn("dbo.RawTransactionStagings", "MCC", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PaymentIndicatorType", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "FilerIndicatorType", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeZIP", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeState", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeCity", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeMailingAddress", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeSecondName", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeFirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "StateIncomeTaxWithheld", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "FederalIncomeTaxWithheld", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "CardPresentTransactions", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeOfficeCode", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "PayeeTIN", c => c.String(maxLength: 255));
            AddColumn("dbo.RawTransactionStagings", "TINType", c => c.String(maxLength: 255));
            AddColumn("dbo.SubmissionDetails", "AccountNo", c => c.String(maxLength: 20));
            AddColumn("dbo.MerchantDetails", "PayeeSecondName", c => c.String(maxLength: 255));
            AddColumn("dbo.MerchantDetails", "PayeeFirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.ImportDetails", "AccountNo", c => c.String(maxLength: 20));
            AlterColumn("dbo.RawTransactionStagings", "TransactionType", c => c.String(maxLength: 255));
            AlterColumn("dbo.RawTransactionStagings", "TransactionDate", c => c.String(maxLength: 255));
            AlterColumn("dbo.RawTransactionStagings", "TransactionAmount", c => c.String(maxLength: 255));
            AlterColumn("dbo.RawTransactionStagings", "PayeeAccountNumber", c => c.String(maxLength: 255));
            AlterColumn("dbo.SubmissionDetails", "CFSF", c => c.String(maxLength: 2));
            AlterColumn("dbo.PayerDetails", "CFSF", c => c.String(maxLength: 2));
            AlterColumn("dbo.MerchantDetails", "CFSF", c => c.String(maxLength: 2));
            AlterColumn("dbo.MerchantDetails", "MCC", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "PaymentIndicatorType", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "FilerIndicatorType", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "PayeeMailingAddress", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "PayeeOfficeCode", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "PayeeTIN", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "TINType", c => c.String(maxLength: 255));
            AlterColumn("dbo.MerchantDetails", "PayeeAccountNumber", c => c.String(maxLength: 255));
            AlterColumn("dbo.ImportDetails", "CFSF", c => c.String(maxLength: 2));
            
        }
    }
}
