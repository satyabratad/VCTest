namespace Bill2Pay.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Structure of Import Detail table
    /// </summary>
    public class ImportDetail : IRSDetail
    {
        /// <summary>
        /// Database identity column
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Client Code
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "Client Code")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Foreign key relation with Import Summary
        /// </summary>
        [ForeignKey("ImportSummary")]
        public int ImportSummaryId { get; set; }

        /// <summary>
        /// Import Summary
        /// </summary>
        public virtual ImportSummary ImportSummary { get; set; }

        /// <summary>
        /// TIN Check Status
        /// </summary>
        [MaxLength(2)]
        [Display(Name ="TIN Status")]
        public string TINCheckStatus { get; set; }

        /// <summary>
        /// TIN Check Remark
        /// </summary>
        [MaxLength(512)]
        public string TINCheckRemarks { get; set; }

        /// <summary>
        /// Foreign key relation with Submission Summary
        /// </summary>
        [ForeignKey("SubmissionSummary")]
        public Nullable<int> SubmissionSummaryId { get; set; }
    
        /// <summary>
        /// Submission Summary
        /// </summary>
        public virtual SubmissionSummary SubmissionSummary { get; set; }

    }
}
