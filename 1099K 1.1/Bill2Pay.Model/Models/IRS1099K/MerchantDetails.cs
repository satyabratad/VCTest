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
        public string PayeeAccountNumber { get; set; }

        [MaxLength(255)]
        public string TINType { get; set; }

        [MaxLength(255)]
        [Display(Name = "TIN")]
        public string PayeeTIN { get; set; }

        [MaxLength(255)]
        [Display(Name = "Office Code")]
        public string PayeeOfficeCode { get; set; }

        [MaxLength(255)]
        [Display(Name = "First Name")]
        public string PayeeFirstName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Second Name")]
        public string PayeeSecondName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Address")]
        public string PayeeMailingAddress { get; set; }

        [MaxLength(255)]
        [Display(Name = "City")]
        public string PayeeCity { get; set; }

        [MaxLength(255)]
        [Display(Name = "State")]
        public string PayeeState { get; set; }

        [MaxLength(255)]
        [Display(Name = "Zip")]
        public string PayeeZIP { get; set; }

        [MaxLength(255)]
        public string FilerIndicatorType { get; set; }

        [MaxLength(255)]
        public string PaymentIndicatorType { get; set; }

        [MaxLength(255)]
        public string MCC { get; set; }

        [MaxLength(2)]
        public string CFSF { get; set; }

        [ForeignKey("Payer")]
        public int PayerId { get; set; }

        public virtual PayerDetail Payer { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }

        [ForeignKey("CreatedUser")]
        public long UserId { get; set; }

        public virtual ApplicationUser CreatedUser { get; set; }

        public int PaymentYear { get; set; }
    }
}
