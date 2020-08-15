using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class ClientSignUp{
        [Required]
        public string Login { get; set; }
        [Required]
        [Email]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
