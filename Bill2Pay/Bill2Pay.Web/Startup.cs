using Bill2Pay.Model;
using Microsoft.Owin;
using Owin;

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
                user.UserName = "arghyab@rssoftware.co.in";
                user.Email = "arghyab@rssoftware.co.in";

                string userPWD = "Admin@123";

                var chkUser = UserManager.CreateAsync(user, userPWD).Result;

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRoleAsync(user.Id, "Admin").Result;

                }

                context.PSEMaster.Add(new PSEMaster()
                {
                    Name = "B2P",
                    Address = "B2P"
                });
                context.SaveChanges();
            }
        }
    }
}
