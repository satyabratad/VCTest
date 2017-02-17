namespace Bill2Pay.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImportDetail : IRSDetail
    {
        [Key, Column(Order = 1)]
        public string AccountNo { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("ImportSummary")]
        public int ImportSummaryId { get; set; }

        public virtual ImportSummary ImportSummary { get; set; }

        [MaxLength(2)]
        public string TINCheckStatus { get; set; }

        [MaxLength(512)]
        public string TINCheckRemarks { get; set; }

        [ForeignKey("SubmissionSummary")]
        public Nullable<int> SubmissionSummaryId { get; set; }
    
        public virtual SubmissionSummary SubmissionSummary { get; set; }

    }
}
