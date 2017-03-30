using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Raw Transaction, is used in Transaction Import process.
    /// </summary>
    public class RawTransaction
    {
        /// <summary>
        /// Database identity column
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Payee Account Number
        /// </summary>
        [MaxLength(20)]
        public string PayeeAccountNumber { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public Nullable<decimal> TransactionAmount { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        [MaxLength(3)]
        public string TransactionType { get; set; }

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
        /// User foreign key
        /// </summary>
        [ForeignKey("CreatedUser")]
        public long UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual ApplicationUser CreatedUser { get; set; }


    }
}