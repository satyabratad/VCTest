using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Application Database Structure
    /// </summary>
    public class ApplicationDbContext
       : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        /// <summary>
        /// Default  Constructor
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        /// <summary>
        /// Import Summary
        /// </summary>
        public virtual DbSet<ImportSummary> ImportSummary { get; set; }

        /// <summary>
        /// Import Details
        /// </summary>
        public virtual DbSet<ImportDetail> ImportDetails { get; set; }

        /// <summary>
        /// Submission Details
        /// </summary>
        public virtual DbSet<SubmissionDetail> SubmissionDetails { get; set; }

        /// <summary>
        /// Submission Summary
        /// </summary>
        public virtual DbSet<SubmissionSummary> SubmissionSummary { get; set; }

        /// <summary>
        /// Submission Status
        /// </summary>
        public virtual DbSet<SubmissionStatus> SubmissionStatus { get; set; }

        /// <summary>
        /// PSE Master
        /// </summary>
        public virtual DbSet<PSEDetails> PSEMaster { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public virtual DbSet<Status> Status { get; set; }

        /// <summary>
        /// TIN Status
        /// </summary>
        public virtual DbSet<TINStatus> TINStatus { get; set; }

        /// <summary>
        /// Raw Transaction Staging
        /// </summary>
        public virtual DbSet<RawTransactionStaging> RawTransactionStaging { get; set; }

        /// <summary>
        /// Raw Transactions
        /// </summary>
        public virtual DbSet<RawTransaction> RawTransactions { get; set; }

        /// <summary>
        /// Merchant Details
        /// </summary>
        public virtual DbSet<MerchantDetails> MerchantDetails { get; set; }

        /// <summary>
        /// Payer Details
        /// </summary>
        public virtual DbSet<PayerDetail> PayerDetails { get; set; }

        /// <summary>
        /// Transmitter Details
        /// </summary>
        public virtual DbSet<TransmitterDetail> TransmitterDetails { get; set; }

        /// <summary>
        /// Create new instence of database
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        static ApplicationDbContext instence = null;

        /// <summary>
        /// Single instence of database
        /// </summary>
        public static ApplicationDbContext Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new ApplicationDbContext();

                }

                if (instence.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    instence.Database.Connection.Open();
                }

                return instence;
            }
        }
    }
}
