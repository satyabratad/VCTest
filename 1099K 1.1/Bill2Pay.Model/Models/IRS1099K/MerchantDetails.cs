using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    public class MerchantDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Display(Name ="Account")]
        
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeAccountNumber { get; set; }

        [MaxLength(1)]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string TINType { get; set; }

        [MaxLength(9)]
        [MinLength(9, ErrorMessage = "TIN must be 9 digit")]
        [Display(Name = "TIN")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TIN must be numeric")]
        
        public string PayeeTIN { get; set; }

        [MaxLength(4)]
        [Display(Name = "Office Code")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeOfficeCode { get; set; }

        [MaxLength(40)]
        [Display(Name = "First Payee Name")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string FirstPayeeName { get; set; }

        [MaxLength(40)]
        [Display(Name = "Second Payee Name")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string SecondPayeeName { get; set; }

        [MaxLength(40)]
        [Display(Name = "Address")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(40)]
        [Display(Name = "City")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeCity { get; set; }

        [MaxLength(2)]
        [Display(Name = "State")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeState { get; set; }

        [MaxLength(9)]
        [Display(Name = "Zip")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Zip must be numeric")]
        
        public string PayeeZIP { get; set; }

        [MaxLength(1)]
        [Display(Name = "FilerIndicator Type")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string FilerIndicatorType { get; set; }


        [MaxLength(1)]
        [Display(Name = "PaymentIndicator Type")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PaymentIndicatorType { get; set; }

        [MaxLength(4)]
        [MinLength(4, ErrorMessage ="MCC must be 4 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "MCC must be numeric")]
        //[RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string MCC { get; set; }

        [MaxLength(1)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string CFSF { get; set; }

        [ForeignKey("Payer")]
        public int PayerId { get; set; }

        public virtual PayerDetail Payer { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        [ForeignKey("CreatedUser")]
        public long UserId { get; set; }

        public virtual ApplicationUser CreatedUser { get; set; }
    }
}
