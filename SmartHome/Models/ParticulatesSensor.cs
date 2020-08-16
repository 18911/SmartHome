using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class ParticulatesSensor : Device{
        IEnumerable<Particulates> listMeasurements { get; set; }

        public ParticulatesSensor(Device device, IEnumerable<Particulates> listMeasurements) : base(device.id_device, device.id_room, device.name, device.type){
            this.listMeasurements = listMeasurements;
        }
    }
}
