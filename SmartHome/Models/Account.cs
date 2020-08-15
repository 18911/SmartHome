using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models{
    public class Account {
        public Client client { get; set; }
        public List<RoomDevices> listRoomDevices { get; set; }
    }
}
