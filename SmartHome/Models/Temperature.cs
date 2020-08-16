using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Temperature{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idTemperature { get; set; }

        [ForeignKey("Device")]
        public int idDevice { get; set; }
        [Required]
        public DateTime mDate { get; set; }
        [Required]
        public DateTime mTime { get; set; }
        [Required]
        public float tValue { get; set; }
    }
}
