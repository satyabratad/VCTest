using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of merchant
    /// </summary>
    public class MerchantDetails
    {
        /// <summary>
        /// Database identity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Client Code
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "Client Code")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PayeeAccountNumber { get; set; }

        /// <summary>
        /// Payee TIN Type
        /// </summary>
        [MaxLength(1)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        [Display(Name ="TIN Type")]
        public string TINType { get; set; }

        /// <summary>
        /// Payee TIN
        /// </summary>
        [MaxLength(9)]
        [MinLength(9, ErrorMessage = "TIN must be 9 digit")]
        [Display(Name = "TIN")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TIN must be numeric")]
        public string PayeeTIN { get; set; }

        /// <summary>
        /// Payee Office  Code
        /// </summary>
        [MaxLength(4)]
        [Display(Name = "Office Code")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PayeeOfficeCode { get; set; }

        /// <summary>
        /// First Payee name
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "First Payee Name")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string FirstPayeeName { get; set; }

        /// <summary>
        /// Second PAyee name
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "Second Payee Name")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string SecondPayeeName { get; set; }

        /// <summary>
        /// Payee address
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "Address")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PayeeMailingAddress { get; set; }

        /// <summary>
        /// Payee city
        /// </summary>
        [MaxLength(40)]
        [Display(Name = "City")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PayeeCity { get; set; }

        /// <summary>
        /// Payee city
        /// </summary>
        [MaxLength(2)]
        [Display(Name = "State")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PayeeState { get; set; }

        /// <summary>
        /// Payee ZIP
        /// </summary>
        [MaxLength(9)]
        [Display(Name = "Zip")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Zip must be numeric")]
        public string PayeeZIP { get; set; }

        /// <summary>
        /// Filer Indicator Type
        /// </summary>
        [MaxLength(1)]
        [Display(Name = "Filer Indicator Type")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string FilerIndicatorType { get; set; }

        /// <summary>
        /// Payment Indicator Type
        /// </summary>
        [MaxLength(1)]
        [Display(Name = "Payment Indicator Type")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string PaymentIndicatorType { get; set; }

        /// <summary>
        /// Merchant Category Code
        /// </summary>
        [MaxLength(4)]
        [MinLength(4, ErrorMessage ="MCC must be 4 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "MCC must be numeric")]
        //[RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string MCC { get; set; }

        /// <summary>
        /// CFSF
        /// </summary>
        [MaxLength(1)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Special Character '<' and  '>' are not allowed.")]
        public string CFSF { get; set; }

        /// <summary>
        /// Foreign key relation with payer
        /// </summary>
        [ForeignKey("Payer")]
        public int PayerId { get; set; }

        /// <summary>
        /// Payer
        /// </summary>
        public virtual PayerDetail Payer { get; set; }

        /// <summary>
        /// Active flag
        /// </summary>
        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Foreign  key with user
        /// </summary>
        [ForeignKey("CreatedUser")]
        public long UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual ApplicationUser CreatedUser { get; set; }
    }
}
