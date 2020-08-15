using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Particulates{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdParticulates { get; set; }

        [ForeignKey("Device")]
        public int IdDevice { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public float Pm25Value { get; set; }
        [Required]
        public float Pm10Value { get; set; }
    }
}
