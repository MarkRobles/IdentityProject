using Microsoft.AspNet.Identity.Owin;
using Model;
using Seervice;
using Seervice.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityProject.Controllers
{
    public class UserController : Controller
    {

        private readonly UserService _userService = new UserService();
        

        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {
            return View(_userService.GetAll());
        }

        public ActionResult Get(string id)
        {
            var roles = _roleManager.Roles.Where(x => x.Enabled).ToList();

            List<SelectListItem> dropDownItems = new List<SelectListItem>();
            foreach (var role in roles)
            {
                SelectListItem item =    new SelectListItem { Value = role.Id, Text = role.Name };
                dropDownItems.Add(item);
            }
            ViewBag.Roles = dropDownItems;

            return View(_userService.Get(id));
        }


        public async Task<ActionResult> AddRoleTouser(string Id, string role) {

            role = "User";
            var roles = await _userManager.GetRolesAsync(Id);
            if (roles.Any())
            {
                throw new Exception("El rol actual ya existe para el usuario");
            }

            await _userManager.AddToRoleAsync(Id,role);

            return RedirectToAction("Index");
        }

        public async Task CreateRoles()
        {

            var roles = new List<ApplicationRole> {
        new ApplicationRole{ Name = "Admin"},
        new ApplicationRole{ Name = "Moderator"},
        new ApplicationRole{ Name = "User"}
        };

            foreach (var role in roles)
            {

                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    var x = await _roleManager.CreateAsync(role);
                }
            }

        }

    }

}
