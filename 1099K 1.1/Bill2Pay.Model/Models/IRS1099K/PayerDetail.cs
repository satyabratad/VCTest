using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Payer details
    /// </summary>
    public class PayerDetail
    {
        /// <summary>
        /// Database identity field
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// CFSF flag
        /// </summary>
        [MaxLength(1)]
        public string CFSF { get; set; }

        /// <summary>
        /// Payer TIN
        /// </summary>
        [MaxLength(9)]
        public string PayerTIN { get; set; }

        /// <summary>
        /// Payer name control
        /// </summary>
        [MaxLength(4)]
        public string PayerNameControl { get; set; }

        /// <summary>
        /// Last Filing Indicator
        /// </summary>
        [MaxLength(1)]
        public string LastFilingIndicator { get; set; }

        /// <summary>
        /// Return Type
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
        /// Foreign key with Transmitter
        /// </summary>
        [ForeignKey("Transmitter")]
        public int TransmitterId { get; set; }

        /// <summary>
        /// Transmitter
        /// </summary>
        public virtual TransmitterDetail Transmitter { get; set; }

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
