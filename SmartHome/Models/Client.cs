using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Client{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdClient { get; set; }
        [Required]
        public string Login { get; set; }
        [Email]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Salt { get; set; }

        public string RefreshToken { get; set; }
    }
}
