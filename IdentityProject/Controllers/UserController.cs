using Seervice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityProject.Controllers
{
    public class UserController : Controller
    {

        private readonly UserService _userService = new UserService();

        public ActionResult Index()
        {
            return View(_userService.GetAll()) ;
        }
    }
}