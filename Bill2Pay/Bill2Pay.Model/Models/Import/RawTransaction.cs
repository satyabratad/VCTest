using System.ComponentModel.DataAnnotations;

namespace Bill2Pay.Model
{
    public abstract class RawTransaction
    {
        [MaxLength(255)]
        public string TINType { get; set; }

        [MaxLength(255)]
        public string PayeeTIN { get; set; }

        [MaxLength(255)]
        public string PayeeAccountNumber { get; set; }

        [MaxLength(255)]
        public string PayeeOfficeCode { get; set; }

        [MaxLength(255)]
        public string CardPresentTransactions { get; set; }

        [MaxLength(255)]
        public string FederalIncomeTaxWithheld { get; set; }

        [MaxLength(255)]
        public string StateIncomeTaxWithheld { get; set; }

        [MaxLength(255)]
        public string TransactionAmount { get; set; }

        [MaxLength(255)]
        public string TransactionDate { get; set; }

        [MaxLength(255)]
        public string TransactionType { get; set; }

        [MaxLength(255)]
        public string PayeeFirstName { get; set; }

        [MaxLength(255)]
        public string PayeeSecondName { get; set; }

        [MaxLength(255)]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(255)]
        public string PayeeCity { get; set; }

        [MaxLength(255)]
        public string PayeeState { get; set; }

        [MaxLength(255)]
        public string PayeeZIP { get; set; }

        [MaxLength(255)]
        public string FilerIndicatorType { get; set; }

        [MaxLength(255)]
        public string PaymentIndicatorType { get; set; }

        [MaxLength(255)]
        public string MCC { get; set; }
    }
}