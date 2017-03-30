using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Bill2Pay.Model;

namespace Bill2Pay.Web.Controllers
{
	[Authorize]
	public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;

        //Default Constructor
        public UsersController()
        {
        }

        //POST : Set Users
        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        //POST :/Manage/ User
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //POST : User /Index
		public ActionResult Index()
		{
            var users = UserManager.GetUsers();

            return View(users);
		}
	}
}