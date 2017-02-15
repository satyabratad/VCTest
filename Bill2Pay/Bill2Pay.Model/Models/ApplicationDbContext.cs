using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.Model
{
    public class ApplicationDbContext
       : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public virtual DbSet<ImportSummary> ImportSummary { get; set; }

        public virtual DbSet<ImportDetail> ImportDetails { get; set; }

        public virtual DbSet<SubmissionDetail> SubmissionDetails { get; set; }

        public virtual DbSet<SubmissionSummary> SubmissionSummary { get; set; }

        public virtual DbSet<SubmissionStatus> SubmissionStatus { get; set; }

        public virtual DbSet<PSEMaster> PSEMaster { get; set; }

        public virtual DbSet<Status> Status { get; set; }

        public virtual DbSet<RawTransactionStaging> RawTransactionStaging { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        static ApplicationDbContext instence = null;
        public static ApplicationDbContext Instence
        {
            get
            {
                if(instence == null)
                {
                    instence = new ApplicationDbContext();
                }

                return instence;
            }
        }
    }
}
