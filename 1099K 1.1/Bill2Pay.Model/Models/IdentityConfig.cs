using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Email Service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Sms Service
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Configure the application user manager which is used in this application.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser, long>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store"></param>
        public ApplicationUserManager(IUserStore<ApplicationUser, long> store)
            : base(store)
        {
        }

        /// <summary>
        /// Create Application User
        /// </summary>
        /// <param name="options">Identity Factory Options</param>
        /// <param name="context">Owin Context</param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicatonUserStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, long>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, long>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> GetUsers()
        {
            return this.Users.ToList();
        }
    }

    /// <summary>
    ///  Configure the application sign-in manager which is used in this application.  
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, long>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">Application User Manager</param>
        /// <param name="authenticationManager">Authentication Manager</param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        /// <summary>
        /// Create User Identity
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>Claims</returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="options">Identity Factory Options</param>
        /// <param name="context">OWin Context</param>
        /// <returns></returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    /// <summary>
    /// Application Role Manager
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole, long>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleStore"></param>
        public ApplicationRoleManager(IRoleStore<ApplicationRole, long> roleStore)
        : base(roleStore) { }

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="options">Identity Factory Options</param>
        /// <param name="context">OWin Context</param>
        /// <returns></returns>
        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
            return manager;
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="role">Application Role</param>
        public void AddRole(ApplicationRole role)
        {
            this.Create(role);
        }

        /// <summary>
        /// Add Role Async
        /// </summary>
        /// <param name="role">Application Role</param>
        /// <returns>Task</returns>
        public async Task AddRoleAsync(ApplicationRole role)
        {
            await this.CreateAsync(role);
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        public List<ApplicationRole> GetRoles()
        {
            return this.Roles.ToList();
        }
    }
}
