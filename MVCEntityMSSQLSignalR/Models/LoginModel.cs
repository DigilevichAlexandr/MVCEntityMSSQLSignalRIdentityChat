using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Models
{
    public class LoginModel
    {
        /// <summary>
        /// User Email
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
