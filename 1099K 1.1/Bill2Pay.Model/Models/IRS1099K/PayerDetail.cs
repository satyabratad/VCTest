using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.Model
{
    public class PayerDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(1)]
        public string CFSF { get; set; }

        [MaxLength(9)]
        public string PayerTIN { get; set; }

        [MaxLength(4)]
        public string PayerNameControl { get; set; }

        [MaxLength(1)]
        public string LastFilingIndicator { get; set; }

        [MaxLength(2)]
        public string ReturnType { get; set; }

        [MaxLength(16)]
        public string AmountCodes { get; set; }

        [MaxLength(1)]
        public string PayerForeignEntityIndicator { get; set; }

        [MaxLength(40)]
        public string FirstPayerName { get; set; }

        [MaxLength(40)]
        public string SecondPayerName { get; set; }

        [MaxLength(1)]
        public string TransferAgentIndicator { get; set; }

        [MaxLength(40)]
        public string PayerShippingAddress { get; set; }

        [MaxLength(40)]
        public string PayerCity { get; set; }

        [MaxLength(2)]
        public string PayerState { get; set; }

        [MaxLength(9)]
        public string PayerZIP { get; set; }

        [MaxLength(15)]
        public string PayerTelephoneNumber { get; set; }

        [ForeignKey("Transmitter")]
        public int TransmitterId { get; set; }

        public virtual TransmitterDetail Transmitter { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        public int PaymentYear { get; set; }
    }
}
