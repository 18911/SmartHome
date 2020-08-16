using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Request{
    public class SignInRequest{
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
    }
}
