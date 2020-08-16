using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class TemperatureSensor : Device{
        IEnumerable<Temperature> listMeasurements { get; set; }
        public TemperatureSensor(Device device,IEnumerable<Temperature> listMeasurements) : base(device.id_device, device.id_room, device.name, device.type) {
            this.listMeasurements = listMeasurements;
        } 
    }
}
