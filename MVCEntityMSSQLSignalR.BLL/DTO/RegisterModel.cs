﻿using System.ComponentModel.DataAnnotations;

namespace MVCEntityMSSQLSignalR.BLL.DTO
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
