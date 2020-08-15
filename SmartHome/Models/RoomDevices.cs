using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class RoomDevices{
        public Room Room { get; set; }
        public List<TemperatureSensor> listTemperatureSensors { get; set; }
        public List<ParticulatesSensor> listParticulatesSensors { get; set; }
    }
}
