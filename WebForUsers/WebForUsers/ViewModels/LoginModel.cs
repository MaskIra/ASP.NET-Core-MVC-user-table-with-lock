using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebForUsers.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
