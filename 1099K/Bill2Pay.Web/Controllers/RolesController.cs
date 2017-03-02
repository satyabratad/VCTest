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
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bill2Pay.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RolesController()
        {
        }

        public RolesController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var roles = RoleManager.GetRoles();
            return View(roles);

        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            var Role = new ApplicationRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(ApplicationRole Role)
        {
            await RoleManager.AddRoleAsync(Role);

            return RedirectToAction("Index");
        }

        /// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
		public async Task<ActionResult> Edit(long ID)
        {
            var role = await RoleManager.FindByIdAsync(ID);
            return View(role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationRole role)
        {
            await RoleManager.UpdateAsync(role);

            return RedirectToAction("Index");
        }
    }
}