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

        [Display(Name = "Gross")]
        public Nullable<decimal> GrossAmount { get; set; }

        [Display(Name = "CNP")]
        public Nullable<decimal> CNPTransactionAmount { get; set; }

        [Display(Name = "FederalWithHolding")]
        public Nullable<decimal> FederalWithHoldingAmount { get; set; }

        [Display(Name = "January")]
        public Nullable<decimal> JanuaryAmount { get; set; }

        [Display(Name = "February")]
        public Nullable<decimal> FebruaryAmount { get; set; }

        [Display(Name = "March")]
        public Nullable<decimal> MarchAmount { get; set; }

        [Display(Name = "April")]
        public Nullable<decimal> AprilAmount { get; set; }

        [Display(Name = "May")]
        public Nullable<decimal> MayAmount { get; set; }

        [Display(Name = "June")]
        public Nullable<decimal> JuneAmount { get; set; }

        [Display(Name = "July")]
        public Nullable<decimal> JulyAmount { get; set; }

        [Display(Name = "August")]
        public Nullable<decimal> AugustAmount { get; set; }

        [Display(Name = "September")]
        public Nullable<decimal> SeptemberAmount { get; set; }

        [Display(Name = "October")]
        public Nullable<decimal> OctoberAmount { get; set; }

        [Display(Name = "November")]
        public Nullable<decimal> NovemberAmount { get; set; }

        [Display(Name = "December")]
        public Nullable<decimal> DecemberAmount { get; set; }

        [MaxLength(1)]
        public string ForeignCountryIndicator { get; set; }

        [MaxLength(40)]
        [Display(Name ="Name")]
        public string FirstPayeeName { get; set; }

        [MaxLength(40)]
        public string SecondPayeeName { get; set; }

        [MaxLength(40)]
        [Display(Name = "Address")]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(40)]
        [Display(Name = "City")]
        public string PayeeCity { get; set; }

        [MaxLength(2)]
        [Display(Name = "State")]
        public string PayeeState { get; set; }

        [MaxLength(9)]
        [Display(Name = "Zip")]
        public string PayeeZipCode { get; set; }

        [MaxLength(1)]
        public string SecondTINNoticed { get; set; }
        [MaxLength(1)]
        public string FillerIndicatorType { get; set; }

        [MaxLength(1)]
        public string PaymentIndicatorType { get; set; }

        [Display(Name = "Count")]
        public int TransactionCount { get; set; }

        [MaxLength(4)]
        [Display(Name = "MCC")]
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

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        [ForeignKey("Merchant")]
        public virtual Nullable<int> MerchantId { get; set; }

        public MerchantDetails Merchant { get; set; }
    }
}