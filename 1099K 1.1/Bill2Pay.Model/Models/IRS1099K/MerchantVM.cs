namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Merchant view
    /// </summary>
    public  class MerchantVM
    {
        /// <summary>
        /// Account number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Selection flag
        /// </summary>
        public int IsChecked { get; set; }
    }
}
