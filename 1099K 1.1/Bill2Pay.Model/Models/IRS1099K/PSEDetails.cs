using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of PSE Details
    /// </summary>
    public class PSEDetails
    {
        /// <summary>
        /// Database identity
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
        /// Total Numberof Payees
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
        /// CFSF
        /// </summary>
        [MaxLength(1)]
        public string CFSF { get; set; }

        /// <summary>
        /// PAyer TIN
        /// </summary>
        [MaxLength(9)]
        public string PayerTIN { get; set; }

        /// <summary>
        /// Payer Name Control
        /// </summary>
        [MaxLength(4)]
        public string PayerNameControl { get; set; }

        /// <summary>
        /// Last Filing Indicator
        /// </summary>
        [MaxLength(1)]
        public string LastFilingIndicator { get; set; }

        /// <summary>
        /// ReturnType 
        /// </summary>
        [MaxLength(2)]
        public string ReturnType { get; set; }

        /// <summary>
        /// Amount Codes
        /// </summary>
        [MaxLength(16)]
        public string AmountCodes { get; set; }

        /// <summary>
        /// Payer Foreign Entity Indicator
        /// </summary>
        [MaxLength(1)]
        public string PayerForeignEntityIndicator { get; set; }

        /// <summary>
        /// First Payer Name
        /// </summary>
        [MaxLength(40)]
        public string FirstPayerName { get; set; }

        /// <summary>
        /// Second Payer Name
        /// </summary>
        [MaxLength(40)]
        public string SecondPayerName { get; set; }

        /// <summary>
        /// Transfer Agent Indicator
        /// </summary>
        [MaxLength(1)]
        public string TransferAgentIndicator { get; set; }

        /// <summary>
        /// Payer Shipping Address
        /// </summary>
        [MaxLength(40)]
        public string PayerShippingAddress { get; set; }

        /// <summary>
        /// Payer City 
        /// </summary>
        [MaxLength(40)]
        public string PayerCity { get; set; }

        /// <summary>
        /// Payer State
        /// </summary>
        [MaxLength(2)]
        public string PayerState { get; set; }

        /// <summary>
        /// Payer ZIP
        /// </summary>
        [MaxLength(9)]
        public string PayerZIP { get; set; }

        /// <summary>
        /// Payer Telephone Number
        /// </summary>
        [MaxLength(15)]
        public string PayerTelephoneNumber { get; set; }

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
        /// relation with Import Details
        /// </summary>
        public virtual ICollection<ImportDetail> ImportDetails { get; set; }

        /// <summary>
        /// Relation  with Submission Details
        /// </summary>
        public virtual ICollection<SubmissionDetail> SubmissionDetails { get; set; }
    }
}
