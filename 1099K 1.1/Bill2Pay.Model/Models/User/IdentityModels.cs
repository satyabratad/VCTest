using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Structure of Application User
    /// </summary>
    public class ApplicationUser : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        /// <summary>
        /// DAtabase Identity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override long Id { get; set; }

        /// <summary>
        /// Default Password flag
        /// </summary>
        public bool IsDefaultPasswordChanged { get; set; }

        /// <summary>
        /// Generate User Identity  
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="authenticationType">Authentication type</param>
        /// <returns>Claims</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Generate User Identity  
        /// </summary>
        /// <param name="manager">User Manager</param>
        /// <returns>Claims</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

   
    /// <summary>
    /// Application Role Store
    /// </summary>
    public class ApplicationRoleStore : RoleStore<ApplicationRole, long, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    /// <summary>
    /// Application Role
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<long>
    {

    }

    /// <summary>
    /// Application User Login
    /// </summary>
    public class ApplicationUserLogin : IdentityUserLogin<long>
    {
    }

    /// <summary>
    /// Application User Claim
    /// </summary>
    public class ApplicationUserClaim : IdentityUserClaim<long>
    {
    }

    /// <summary>
    /// Application Role
    /// </summary>
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
    }

    /// <summary>
    /// Application User Store
    /// </summary>
    public class ApplicatonUserStore :
        UserStore<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicatonUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}