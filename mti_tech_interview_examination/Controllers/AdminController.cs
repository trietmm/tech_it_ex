using mti_tech_interview_examination.Common;
using mti_tech_interview_examination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mti_tech_interview_examination.Controllers
{
    /// <summary>
    /// Admin region
    /// </summary>
    [CustomAuthentication("admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>    
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("CreateQuestion");
            return View();
        }

        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && FormsAuthentication.Authenticate(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                //save UserName to session
                Session[SessionKey.UserName] = model.UserName;

                return RedirectToAction("CreateQuestion");
            }

            return View(model);
        }

        /// <summary>
        /// Create question form
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQuestion()
        {
            return View();
        }
    }
}