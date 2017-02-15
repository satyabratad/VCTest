//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bill2Pay.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubmissionStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int PaymentYear { get; set; }

        [MaxLength(255)]
        public string AccountNumber { get; set; }

        public DateTime ProcessingDate { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
    }
}
