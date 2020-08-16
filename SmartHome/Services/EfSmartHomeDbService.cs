using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using SmartHome.Contexts;
using SmartHome.Models;
using SmartHome.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services{
    public class EfSmartHomeDbService : ISmartHomeDbService{
        private readonly SmartHomeDbContext dbContext;

        public EfSmartHomeDbService(SmartHomeDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Client Register(ClientSignUp clientSignUp){
            Client client = new Client();

            if (dbContext.Client.Where(c => c.Login == clientSignUp.Login).Any()){
                client = null;
            }else {
                Client cTmp = dbContext.Client.FirstOrDefault();
            
                if (cTmp != null){
                    Console.WriteLine(cTmp.IdClient);
                    var maxId = dbContext.Client.Max(c => c.IdClient);
                    client.IdClient = maxId + 1;
                }
                else {
                    Console.WriteLine("baza jest pusta");
                    client.IdClient = 0;
                }
                var salt = CreateSalt();
                var password = Create(clientSignUp.Password, salt);
                client.Login = clientSignUp.Login;
                client.Email = clientSignUp.Email;
                client.Password = password;
                client.Salt = salt;
                
                dbContext.Client.Add(client);
                //dbContext.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT Client ON");
                dbContext.SaveChanges();
            }
            return client;
        }

        public string Create(string value, string salt) {
            var valueBytes = KeyDerivation.Pbkdf2(
                    password: value,
                    salt: Encoding.UTF8.GetBytes(salt),
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                );
            return Convert.ToBase64String(valueBytes);
        }


        public string CreateSalt(){
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create()){
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public bool Validate(string value, string salt, string hash) => Create(value, salt) == hash;

        public Client Login(SignInRequest signInRequest){
            Client client = new Client();
            if (dbContext.Client.Where(c => c.Login == signInRequest.Login).Any()) {
                client = dbContext.Client.SingleOrDefault(c => c.Login == signInRequest.Login);
                if (!Validate(signInRequest.Password, client.Salt, client.Password)){
                    client = null;
                }
            }
            else{
                client = null;
            }
            return client;
        }

        public Account GetAccount(Client client){

            Console.WriteLine("Ef get account 1");

            Account account = new Account();

            account.client = client;

            var ListRooms = dbContext.Room.Where(r => r.IdClient == client.IdClient).
                Select(room => new Room { IdRoom = room.IdRoom, IdClient = room.IdClient, Name = room.Name }).ToList();

            Console.WriteLine("Ef get account 2");

            var ListRoomDevices = new List<RoomDevices>();

            foreach (Room room in ListRooms) {
                var RoomDevices = new RoomDevices();
                RoomDevices.listParticulatesSensors = new List<ParticulatesSensor>();
                RoomDevices.listTemperatureSensors = new List<TemperatureSensor>();
                RoomDevices.Room = room;
                var ListDevices = dbContext.Device.Where(d => d.IdRoom == room.IdRoom).
                    Select(device => new Device (device.IdDevice,device.IdRoom,device.Name,device.Type )).ToList();
                
                foreach (Device device in ListDevices) {
                    if (device.Type == DeviceTypes.TemperatureSensor.ToString()){
                        var ListMeasurements = dbContext.Temperature.Where(t => t.IdDevice == device.IdDevice).
                            Select(temp => new Temperature { IdTemperature = temp.IdTemperature, IdDevice = temp.IdDevice, TValue = temp.TValue, MDate = temp.MDate, MTime = temp.MTime }).ToList();
                        var temperatureSensor = new TemperatureSensor(device, ListMeasurements);
                        RoomDevices.listTemperatureSensors.Add(temperatureSensor);
                    }
                    else {
                        var ListMeasurements = dbContext.Particulates.Where(p => p.IdDevice == device.IdDevice).
                            Select(part => new Particulates { IdParticulates = part.IdParticulates, IdDevice = part.IdDevice, Pm25Value = part.Pm25Value,Pm10Value = part.Pm10Value ,Date = part.Date, Time = part.Time }).ToList();
                        var particualtesSensor = new ParticulatesSensor(device, ListMeasurements);
                        RoomDevices.listParticulatesSensors.Add(particualtesSensor);
                    }
                }
                ListRoomDevices.Add(RoomDevices);
            }

            Console.WriteLine("Ef get account 3");

            account.listRoomDevices = ListRoomDevices;

            Console.WriteLine(account.client.Login + " " + account.listRoomDevices);

            return account;
        }

        public IEnumerable<Temperature> GetTemperature() {
            return dbContext.Temperature.Select(temp => new Temperature { IdTemperature = temp.IdTemperature, IdDevice = temp.IdDevice, TValue = temp.TValue, MDate = temp.MDate, MTime = temp.MTime });
        }
    }
}
