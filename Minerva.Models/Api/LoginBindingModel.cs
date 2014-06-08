using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Minerva.ApiControllers
{
    public class LoginBindingModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}
