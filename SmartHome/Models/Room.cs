using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Room{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idRoom { get; set; }
        [ForeignKey("Client")]
        public int idClient { get; set; }
        [Required]
        public string name { get; set; }
    }
}
