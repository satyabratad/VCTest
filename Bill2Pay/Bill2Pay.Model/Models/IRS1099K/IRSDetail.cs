using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    public abstract class IRSDetail
    {
        [MaxLength(1)]
        public string TINType { get; set; }

        [MaxLength(9)]
        public string TIN { get; set; }

        [MaxLength(4)]
        public string PayerOfficeCode { get; set; }

        public Nullable<decimal> GrossAmount { get; set; }

        public Nullable<decimal> CNPTransactionAmount { get; set; }

        public Nullable<decimal> FederalWithHoldingAmount { get; set; }

        public Nullable<decimal> JanuaryAmount { get; set; }

        public Nullable<decimal> FebruaryAmount { get; set; }

        public Nullable<decimal> MarchAmount { get; set; }

        public Nullable<decimal> AprilAmount { get; set; }

        public Nullable<decimal> MayAmount { get; set; }

        public Nullable<decimal> JuneAmount { get; set; }

        public Nullable<decimal> JulyAmount { get; set; }

        public Nullable<decimal> AugustAmount { get; set; }

        public Nullable<decimal> SeptemberAmount { get; set; }

        public Nullable<decimal> OctoberAmount { get; set; }

        public Nullable<decimal> NovemberAmount { get; set; }

        public Nullable<decimal> DecemberAmount { get; set; }

        [MaxLength(1)]
        public string ForeignCountryIndicator { get; set; }

        [MaxLength(40)]
        public string FirstPayeeName { get; set; }

        [MaxLength(40)]
        public string SecondPayeeName { get; set; }

        [MaxLength(40)]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(40)]
        public string PayeeCity { get; set; }

        [MaxLength(2)]
        public string PayeeState { get; set; }

        [MaxLength(9)]
        public string PayeeZipCode { get; set; }

        [MaxLength(1)]
        public string SecondTINNoticed { get; set; }
        [MaxLength(1)]
        public string FillerIndicatorType { get; set; }

        [MaxLength(1)]
        public string PaymentIndicatorType { get; set; }

        public int TransactionCount { get; set; }

        [ForeignKey("PSE")]
        public virtual Nullable<int> PseId { get; set; }

        public PSEDetails PSE { get; set; }

        [MaxLength(4)]
        public string MerchantCategoryCode { get; set; }

        [MaxLength(60)]
        public string SpecialDataEntry { get; set; }

        public Nullable<decimal> StateWithHolding { get; set; }

        public Nullable<decimal> LocalWithHolding { get; set; }

        [MaxLength(2)]
        public string CFSF { get; set; }

        [MaxLength(4)]
        public string NameControl { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }


    }
}