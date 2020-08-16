using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class ClientSignUp{
        [Required]
        public string login { get; set; }
        [Required]
        [Email]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
