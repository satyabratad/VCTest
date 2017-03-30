using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Bill2Pay.Model;

namespace Bill2Pay.Web.Controllers
{
    /// <summary>
    /// User controller manage the user
    /// Get, Set and Display list of users.
    /// </summary>
	[Authorize]
	public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Default Contructor
        /// </summary>
        public UsersController()
        {
        }

        /// <summary>
        /// POST method.
        /// Set user manager value  
        /// </summary>
        /// <param name="userManager">Application User Manager</param>
        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Public Property
        /// GET/SET User Manager value
        /// Returns ApplicationUserManager
        /// </summary>
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

        /// <summary>
        /// Action Method
        /// </summary>
        /// <returns>ActionResults</returns>
		public ActionResult Index()
		{
            var users = UserManager.GetUsers();

            return View(users);
		}
	}
}