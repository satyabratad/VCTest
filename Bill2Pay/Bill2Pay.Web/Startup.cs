using Bill2Pay.Model;
using Microsoft.Owin;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Bill2Pay.Web.Startup))]
namespace Bill2Pay.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
            var UserManager = new ApplicationUserManager(new ApplicatonUserStore(context));


            // In Startup iam creating first Admin Role and creating a default Admin User 
            var HasRole = roleManager.RoleExistsAsync("Admin").Result;

            if (!HasRole)
            {

                // first we create Admin rool
                var role = new ApplicationRole();
                role.Name = "Admin";
                var result = roleManager.CreateAsync(role).Result;

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "admin@b2p.com"; 
                user.Email = "admin@b2p.com";

                string userPWD = "Admin@123";

                var chkUser = UserManager.CreateAsync(user, userPWD).Result;

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRoleAsync(user.Id, "Admin").Result;

                }

                context.SaveChanges();
            }

            // In Startup iam creating first Admin Role and creating a default Admin User 
            HasRole = roleManager.RoleExistsAsync("User").Result;

            if (!HasRole)
            {

                // first we create Admin rool
                var role = new ApplicationRole();
                role.Name = "User";
                var result = roleManager.CreateAsync(role).Result;

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "user@b2p.com";
                user.Email = "user@b2p.com";

                string userPWD = "User@123";

                var chkUser = UserManager.CreateAsync(user, userPWD).Result;

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRoleAsync(user.Id, "User").Result;

                }

                
                context.SaveChanges();
            }

            
        }
    }
}
