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
        public int IdDevice { get; set; }

        [ForeignKey("Room")]
        public int IdRoom { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public Device(int IdDevice,int IdRoom, string Name, string Type) {
            this.IdDevice = IdDevice;
            this.IdRoom = IdRoom;
            this.Name = Name;
            this.Type = Type;
        }
    }
}
