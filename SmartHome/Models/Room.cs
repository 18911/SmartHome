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
        public int id_room { get; set; }
        [ForeignKey("Client")]
        public int id_client { get; set; }
        [Required]
        public string name { get; set; }
    }
}
