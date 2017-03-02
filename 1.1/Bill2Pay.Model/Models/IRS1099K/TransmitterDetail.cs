using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.Model
{
    public class TransmitterDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(9)]
        public string TransmitterTIN { get; set; }

        [MaxLength(5)]
        public string TransmitterControlCode { get; set; }

        [MaxLength(1)]
        public string TestFileIndicator { get; set; }

        [MaxLength(1)]
        public string TransmitterForeignEntityIndicator { get; set; }

        [MaxLength(40)]
        public string TransmitterName { get; set; }

        [MaxLength(40)]
        public string TransmitterNameContinued { get; set; }

        [MaxLength(40)]
        public string CompanyName { get; set; }

        [MaxLength(40)]
        public string CompanyNameContinued { get; set; }

        [MaxLength(40)]
        public string CompanyMailingAddress { get; set; }

        [MaxLength(40)]
        public string CompanyCity { get; set; }

        [MaxLength(2)]
        public string CompanyState { get; set; }

        [MaxLength(9)]
        public string CompanyZIP { get; set; }

        public int TotalNumberofPayees { get; set; }

        [MaxLength(40)]
        public string ContactName { get; set; }

        [MaxLength(15)]
        public string ContactTelephoneNumber { get; set; }

        [MaxLength(50)]
        public string ContactEmailAddress { get; set; }

        [MaxLength(1)]
        public string VendorIndicator { get; set; }

        [MaxLength(40)]
        public string VendorName { get; set; }

        [MaxLength(40)]
        public string VendorMailingAddress { get; set; }

        [MaxLength(40)]
        public string VendorCity { get; set; }

        [MaxLength(2)]
        public string VendorState { get; set; }

        [MaxLength(9)]
        public string VendorZIP { get; set; }

        [MaxLength(40)]
        public string VendorContactName { get; set; }

        [MaxLength(15)]
        public string VendorContactTelephoneNumber { get; set; }

        [MaxLength(1)]
        public string VendorForeignEntityIndicator { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }

        public int PaymentYear { get; set; }

    }
}
