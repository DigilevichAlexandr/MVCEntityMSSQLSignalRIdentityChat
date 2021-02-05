using System;
using System.ComponentModel.DataAnnotations;

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
