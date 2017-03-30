using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Transmitter Details
    /// </summary>
    public class TransmitterDetail
    {
        /// <summary>
        /// Database Identity 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Transmitter TIN
        /// </summary>
        [MaxLength(9)]
        public string TransmitterTIN { get; set; }

        /// <summary>
        /// Transmitter Control Code
        /// </summary>
        [MaxLength(5)]
        public string TransmitterControlCode { get; set; }

        /// <summary>
        /// Test File Indicator
        /// </summary>
        [MaxLength(1)]
        public string TestFileIndicator { get; set; }

        /// <summary>
        /// Transmitter Foreign Entity Indicator
        /// </summary>
        [MaxLength(1)]
        public string TransmitterForeignEntityIndicator { get; set; }

        /// <summary>
        /// Transmitter Name
        /// </summary>
        [MaxLength(40)]
        public string TransmitterName { get; set; }

        /// <summary>
        /// Transmitter Name Continued
        /// </summary>
        [MaxLength(40)]
        public string TransmitterNameContinued { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        [MaxLength(40)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Company Name Continued
        /// </summary>
        [MaxLength(40)]
        public string CompanyNameContinued { get; set; }

        /// <summary>
        /// Company Mailing Address
        /// </summary>
        [MaxLength(40)]
        public string CompanyMailingAddress { get; set; }

        /// <summary>
        /// Company City
        /// </summary>
        [MaxLength(40)]
        public string CompanyCity { get; set; }

        /// <summary>
        /// Company State
        /// </summary>
        [MaxLength(2)]
        public string CompanyState { get; set; }

        /// <summary>
        /// Company ZIP
        /// </summary>
        [MaxLength(9)]
        public string CompanyZIP { get; set; }

        /// <summary>
        /// Total Number of Payees
        /// </summary>
        public int TotalNumberofPayees { get; set; }

        /// <summary>
        /// Contact Name
        /// </summary>
        [MaxLength(40)]
        public string ContactName { get; set; }

        /// <summary>
        /// Contact Telephone Number
        /// </summary>
        [MaxLength(15)]
        public string ContactTelephoneNumber { get; set; }

        /// <summary>
        /// Contact Email Address
        /// </summary>
        [MaxLength(50)]
        public string ContactEmailAddress { get; set; }

        /// <summary>
        /// Vendor Indicator
        /// </summary>
        [MaxLength(1)]
        public string VendorIndicator { get; set; }

        /// <summary>
        /// Vendor Name
        /// </summary>
        [MaxLength(40)]
        public string VendorName { get; set; }

        /// <summary>
        /// Vendor Mailing Address
        /// </summary>
        [MaxLength(40)]
        public string VendorMailingAddress { get; set; }

        /// <summary>
        /// Vendor City
        /// </summary>
        [MaxLength(40)]
        public string VendorCity { get; set; }

        /// <summary>
        /// Vendor State
        /// </summary>
        [MaxLength(2)]
        public string VendorState { get; set; }

        /// <summary>
        /// Vendor ZIP
        /// </summary>
        [MaxLength(9)]
        public string VendorZIP { get; set; }

        /// <summary>
        /// Vendor Contact Name
        /// </summary>
        [MaxLength(40)]
        public string VendorContactName { get; set; }

        /// <summary>
        /// Vendor Contact Telephone Number
        /// </summary>
        [MaxLength(15)]
        public string VendorContactTelephoneNumber { get; set; }

        /// <summary>
        /// Vendor Foreign Entity Indicator
        /// </summary>
        [MaxLength(1)]
        public string VendorForeignEntityIndicator { get; set; }

        /// <summary>
        /// Active  flag
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date of addition
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Payment Year
        /// </summary>
        public int PaymentYear { get; set; }

    }
}
