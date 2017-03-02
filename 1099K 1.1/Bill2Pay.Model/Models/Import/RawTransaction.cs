using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill2Pay.Model
{
    public class RawTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string PayeeAccountNumber { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
       
        [ForeignKey("CreatedUser")]
        public long UserId { get; set; }

        public virtual ApplicationUser CreatedUser { get; set; }


    }
}