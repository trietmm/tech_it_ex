using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace mti_tech_interview_examination.Common
{
    /// <summary>
    /// Custom authentication
    /// </summary>
    public class CustomAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        private List<string> _AllowedNames = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomAuthenticationAttribute()
        {
        }

        /// <summary>
        /// Constructor with list of names which are seperated by commas
        /// </summary>
        /// <param name="name"></param>
        public CustomAuthenticationAttribute(string name)
        {
            //Get list of allowed names
            if (!string.IsNullOrEmpty(name))
            {
                _AllowedNames = name.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }
        /// <summary>
        /// OnAuthentication
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        /// <summary>
        /// OnAuthenticationChallenge
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if ((!user.Identity.IsAuthenticated  || (_AllowedNames.Count > 0 && !_AllowedNames.Contains(filterContext.HttpContext.User.Identity.Name))) &&
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Count() == 0)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }

}