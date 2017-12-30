using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Login model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [Display(Name ="User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}