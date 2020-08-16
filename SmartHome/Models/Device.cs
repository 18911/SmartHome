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
        public int idDevice { get; set; }

        [ForeignKey("Room")]
        public int idRoom { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string type { get; set; }

        public Device(int idDevice,int idRoom, string name, string type) {
            this.idDevice = idDevice;
            this.idRoom = idRoom;
            this.name = name;
            this.type = type;
        }
    }
}
