namespace Bill2Pay.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImportDetail : IRSDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Display(Name = "Account")]
        public string AccountNumber { get; set; }

        [ForeignKey("ImportSummary")]
        public int ImportSummaryId { get; set; }

        public virtual ImportSummary ImportSummary { get; set; }

        [MaxLength(2)]
        [Display(Name ="TIN Status")]
        public string TINCheckStatus { get; set; }

        [MaxLength(512)]
        public string TINCheckRemarks { get; set; }

        [ForeignKey("SubmissionSummary")]
        public Nullable<int> SubmissionSummaryId { get; set; }
    
        public virtual SubmissionSummary SubmissionSummary { get; set; }

    }
}
