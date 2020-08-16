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
        public int id_temperature { get; set; }

        [ForeignKey("Device")]
        public int id_device { get; set; }
        [Required]
        public float t_value { get; set; }
        [Required]
        public DateTime m_date { get; set; }
        [Required]
        public DateTime m_time { get; set; }
    }
}
