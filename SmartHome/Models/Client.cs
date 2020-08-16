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
        public int idClient { get; set; }
        [Required]
        public string login { get; set; }
        [Email]
        public string email { get; set; }
        [Required]
        public string password { get; set; }

        public string salt { get; set; }

        public string refreshToken { get; set; }
    }
}
