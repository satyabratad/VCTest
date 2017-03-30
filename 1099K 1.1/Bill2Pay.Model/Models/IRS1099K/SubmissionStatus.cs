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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Submission Status
    /// </summary>
    public class SubmissionStatus
    {
        /// <summary>
        /// Database identity 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Payment Year
        /// </summary>
        public int PaymentYear { get; set; }

        /// <summary>
        /// Client Code
        /// </summary>
        [Display(Name = "Client Code")]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Date of processing
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime ProcessingDate { get; set; }

        /// <summary>
        /// Foreign key relation with Status
        /// </summary>
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public virtual Status Status { get; set; }

        /// <summary>
        /// Active flag
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date of addition
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }
    }
}
