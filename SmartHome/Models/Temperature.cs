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
        public int IdTemperature { get; set; }

        [ForeignKey("Device")]
        public int IdDevice { get; set; }
        [Required]
        public DateTime MDate { get; set; }
        [Required]
        public DateTime MTime { get; set; }
        [Required]
        public float TValue { get; set; }
    }
}
