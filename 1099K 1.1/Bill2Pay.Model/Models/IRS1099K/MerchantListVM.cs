namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Merchant Listing view
    /// </summary>
    public class MerchantListVM
    {
        /// <summary>
        /// Import details
        /// </summary>
        public ImportDetail ImportDetails { get; set; }

        /// <summary>
        /// Submission Status
        /// </summary>
        public SubmissionStatus SubmissionStatus { get; set; }

    }
}
