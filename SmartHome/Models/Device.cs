using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Device{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_device { get; set; }

        [ForeignKey("Room")]
        public int id_room { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string type { get; set; }

        public Device(int id_device,int id_room, string name, string type) {
            this.id_device = id_device;
            this.id_room = id_room;
            this.name = name;
            this.type = type;
        }
    }
}
