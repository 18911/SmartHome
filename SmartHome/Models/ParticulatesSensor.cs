using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class ParticulatesSensor : Device{
        IEnumerable<Particulates> listMeasurements { get; set; }

        public ParticulatesSensor(Device device, IEnumerable<Particulates> listMeasurements) : base(device.IdDevice, device.IdRoom, device.Name, device.Type){
            this.listMeasurements = listMeasurements;
        }
    }
}
