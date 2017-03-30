namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of merchant details view 
    /// </summary>
    public class MerchantDetailsVM
    {
        /// <summary>
        /// Merchant
        /// </summary>
        public MerchantDetails Merchant { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public SubmissionStatus Status { get; set; }

    }
}
