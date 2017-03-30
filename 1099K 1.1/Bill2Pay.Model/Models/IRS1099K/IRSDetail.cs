using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of IRS details
    /// </summary>
    public abstract class IRSDetail
    {
        /// <summary>
        /// TIN Type
        /// </summary>
        [MaxLength(1)]
        public string TINType { get; set; }

        /// <summary>
        /// TIN
        /// </summary>
        [MaxLength(9)]
        public string TIN { get; set; }

        /// <summary>
        /// Payer Office Code
        /// </summary>
        [MaxLength(4)]
        public string PayerOfficeCode { get; set; }

        /// <summary>
        /// Gross amount
        /// </summary>
        [Display(Name = "Gross")]
        public Nullable<decimal> GrossAmount { get; set; }

        /// <summary>
        /// CNP Transaction amount
        /// </summary>
        [Display(Name = "CNP")]
        public Nullable<decimal> CNPTransactionAmount { get; set; }

        /// <summary>
        /// Federal WithHolding amount
        /// </summary>
        [Display(Name = "Federal WithHolding")]
        public Nullable<decimal> FederalWithHoldingAmount { get; set; }

        /// <summary>
        /// January amount
        /// </summary>
        [Display(Name = "January")]
        public Nullable<decimal> JanuaryAmount { get; set; }

        /// <summary>
        /// February amount
        /// </summary>
        [Display(Name = "February")]
        public Nullable<decimal> FebruaryAmount { get; set; }

        /// <summary>
        /// March  amount
        /// </summary>
        [Display(Name = "March")]
        public Nullable<decimal> MarchAmount { get; set; }

        /// <summary>
        /// April amount
        /// </summary>
        [Display(Name = "April")]
        public Nullable<decimal> AprilAmount { get; set; }

        /// <summary>
        /// May amount
        /// </summary>
        [Display(Name = "May")]
        public Nullable<decimal> MayAmount { get; set; }

        /// <summary>
        /// June amount
        /// </summary>
        [Display(Name = "June")]
        public Nullable<decimal> JuneAmount { get; set; }

        /// <summary>
        /// July amount
        /// </summary>
        [Display(Name = "July")]
        public Nullable<decimal> JulyAmount { get; set; }

        /// <summary>
        /// August amount
        /// </summary>
        [Display(Name = "August")]
        public Nullable<decimal> AugustAmount { get; set; }

        /// <summary>
        /// September amount
        /// </summary>
        [Display(Name = "September")]
        public Nullable<decimal> SeptemberAmount { get; set; }

        /// <summary>
        /// October amount
        /// </summary>
        [Display(Name = "October")]
        public Nullable<decimal> OctoberAmount { get; set; }

        /// <summary>
        /// November amount
        /// </summary>
        [Display(Name = "November")]
        public Nullable<decimal> NovemberAmount { get; set; }

        /// <summary>
        /// December amount
        /// </summary>
        [Display(Name = "December")]
        public Nullable<decimal> DecemberAmount { get; set; }

        /// <summary>
        /// Foreign Country Indicator
        /// </summary>
        [MaxLength(1)]
        public string ForeignCountryIndicator { get; set; }

        /// <summary>
        /// First payee name
        /// </summary>
        [MaxLength(40)]
        [Display(Name ="Name")]
        public string FirstPayeeName { get; set; }

        /// <summary>
        /// Second payee name
        /// </summary>
        [MaxLength(40)]
        public string SecondPayeeName { get; set; }

        /// <summary>
        /// Payee mailing address
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "Address")]
        public string PayeeMailingAddress { get; set; }

        /// <summary>
        /// Payee city
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "City")]
        public string PayeeCity { get; set; }

        /// <summary>
        /// PAyee State
        /// </summary>
        [MaxLength(2)]
        [Display(Name = "State")]
        public string PayeeState { get; set; }

        /// <summary>
        /// Payee ZIP
        /// </summary>
        [MaxLength(9)]
        [Display(Name = "Zip")]
        public string PayeeZipCode { get; set; }

        /// <summary>
        /// Second TIN Noticed
        /// </summary>
        [MaxLength(1)]
        public string SecondTINNoticed { get; set; }

        /// <summary>
        /// Filler Indicator Type
        /// </summary>
        [MaxLength(1)]
        public string FillerIndicatorType { get; set; }

        /// <summary>
        /// Payment Indicator Type
        /// </summary>
        [MaxLength(1)]
        public string PaymentIndicatorType { get; set; }

        /// <summary>
        /// Transaction Count
        /// </summary>
        [Display(Name = "Count")]
        public int TransactionCount { get; set; }

        /// <summary>
        /// Merchant Category Code
        /// </summary>
        [MaxLength(4)]
        [Display(Name = "MCC")]
        public string MerchantCategoryCode { get; set; }

        /// <summary>
        /// Special DataEntry field for  IRS
        /// </summary>
        [MaxLength(60)]
        public string SpecialDataEntry { get; set; }

        /// <summary>
        /// State WithHolding amount
        /// </summary>
        public Nullable<decimal> StateWithHolding { get; set; }

        /// <summary>
        /// Local WithHolding amount
        /// </summary>
        public Nullable<decimal> LocalWithHolding { get; set; }

        /// <summary>
        /// CFSF
        /// </summary>
        [MaxLength(1)]
        public string CFSF { get; set; }

        /// <summary>
        /// Name Control
        /// </summary>
        [MaxLength(4)]
        public string NameControl { get; set; }

        /// <summary>
        /// Active flag
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date of addition
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Foreign key with merchant
        /// </summary>
        [ForeignKey("Merchant")]
        public virtual Nullable<int> MerchantId { get; set; }

        /// <summary>
        /// Merchant
        /// </summary>
        public MerchantDetails Merchant { get; set; }
    }
}