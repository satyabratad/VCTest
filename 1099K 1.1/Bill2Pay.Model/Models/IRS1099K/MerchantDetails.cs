using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.Model
{
    public class MerchantDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name ="Account")]
        
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeAccountNumber { get; set; }

        [MaxLength(255)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string TINType { get; set; }

        [MaxLength(255)]
        [Display(Name = "TIN")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeTIN { get; set; }

        [MaxLength(255)]
        [Display(Name = "Office Code")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeOfficeCode { get; set; }

        [MaxLength(255)]
        [Display(Name = "First Name")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeFirstName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Second Name")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeSecondName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Address")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(40)]
        [Display(Name = "City")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeCity { get; set; }

        [MaxLength(2)]
        [Display(Name = "State")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeState { get; set; }

        [MaxLength(9)]
        [Display(Name = "Zip")]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeZIP { get; set; }

        [MaxLength(255)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string FilerIndicatorType { get; set; }


        [MaxLength(255)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PaymentIndicatorType { get; set; }

        [MaxLength(255)]
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string MCC { get; set; }

        [MaxLength(2)]
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

        public int PaymentYear { get; set; }
    }
}
