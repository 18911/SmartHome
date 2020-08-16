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
        public int idParticulates { get; set; }

        [ForeignKey("Device")]
        public int idDevice { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public DateTime time { get; set; }
        [Required]
        public float pm25Value { get; set; }
        [Required]
        public float pm10Value { get; set; }
    }
}
