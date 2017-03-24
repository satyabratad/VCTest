﻿using System;
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

        [MaxLength(20)]
        [Display(Name ="Account")]
        
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string PayeeAccountNumber { get; set; }

        [MaxLength(1)]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
        public string TINType { get; set; }

        [MaxLength(9)]
        [Display(Name = "TIN")]
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
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
        [RegularExpression("^[^<>,<|>]+$", ErrorMessage = "Html tags are not allowed.")]
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
        [RegularExpression("^[^<><|>]+$", ErrorMessage = "Html tags are not allowed.")]
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
