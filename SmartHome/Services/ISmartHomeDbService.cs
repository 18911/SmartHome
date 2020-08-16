using SmartHome.Models;
using SmartHome.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Services{
    public interface ISmartHomeDbService{
        public Client Register(ClientSignUp client);
        public Client Login(SignInRequest signInRequest);
        public Account GetAccount(Client client);
        public IEnumerable<Temperature> GetTemperature();
    }
}
