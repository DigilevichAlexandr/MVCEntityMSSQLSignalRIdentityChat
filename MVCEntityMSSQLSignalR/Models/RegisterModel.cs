using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Models
{
    public class RegisterModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Enter the Email")]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not match")]
        public string ConfirmPassword { get; set; }
    }
}
