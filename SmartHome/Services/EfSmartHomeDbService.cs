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

            if (dbContext.client.Where(c => c.login == clientSignUp.Login).Any()){
                client = null;
            }else {
                Client cTmp = dbContext.client.FirstOrDefault();
            
                if (cTmp != null){
                    Console.WriteLine(cTmp.idClient);
                    var maxId = dbContext.client.Max(c => c.idClient);
                    client.idClient = maxId + 1;
                }
                else {
                    Console.WriteLine("baza jest pusta");
                    client.idClient = 0;
                }
                var salt = CreateSalt();
                var password = Create(clientSignUp.Password, salt);
                client.login = clientSignUp.Login;
                client.email = clientSignUp.Email;
                client.password = password;
                client.salt = salt;
                
                dbContext.client.Add(client);
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
            if (dbContext.client.Where(c => c.login == signInRequest.Login).Any()) {
                client = dbContext.client.SingleOrDefault(c => c.login == signInRequest.Login);
                if (!Validate(signInRequest.Password, client.salt, client.password)){
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

            var ListRooms = dbContext.room.Where(r => r.idClient == client.idClient).
                Select(room => new Room { idRoom = room.idRoom, idClient = room.idClient, name = room.name }).ToList();

            Console.WriteLine("Ef get account 2");

            var ListRoomDevices = new List<RoomDevices>();

            foreach (Room room in ListRooms) {
                var RoomDevices = new RoomDevices();
                RoomDevices.listParticulatesSensors = new List<ParticulatesSensor>();
                RoomDevices.listTemperatureSensors = new List<TemperatureSensor>();
                RoomDevices.Room = room;
                var ListDevices = dbContext.device.Where(d => d.idRoom == room.idRoom).
                    Select(device => new Device (device.idDevice,device.idRoom,device.name,device.type )).ToList();
                
                foreach (Device device in ListDevices) {
                    if (device.type == DeviceTypes.TemperatureSensor.ToString()){
                        var ListMeasurements = dbContext.temperature.Where(t => t.idDevice == device.idDevice).
                            Select(temp => new Temperature { idTemperature = temp.idTemperature, idDevice = temp.idDevice, tValue = temp.tValue, mDate = temp.mDate, mTime = temp.mTime }).ToList();
                        var temperatureSensor = new TemperatureSensor(device, ListMeasurements);
                        RoomDevices.listTemperatureSensors.Add(temperatureSensor);
                    }
                    else {
                        var ListMeasurements = dbContext.particulates.Where(p => p.idDevice == device.idDevice).
                            Select(part => new Particulates { idParticulates = part.idParticulates, idDevice = part.idDevice, pm25Value = part.pm25Value,pm10Value = part.pm10Value ,date = part.date, time = part.time }).ToList();
                        var particualtesSensor = new ParticulatesSensor(device, ListMeasurements);
                        RoomDevices.listParticulatesSensors.Add(particualtesSensor);
                    }
                }
                ListRoomDevices.Add(RoomDevices);
            }

            Console.WriteLine("Ef get account 3");

            account.listRoomDevices = ListRoomDevices;

            Console.WriteLine(account.client.login + " " + account.listRoomDevices);

            return account;
        }

        public IEnumerable<Temperature> GetTemperature() {
            return dbContext.temperature.Select(temp => new Temperature { idTemperature = temp.idTemperature, idDevice = temp.idDevice, tValue = temp.tValue, mDate = temp.mDate, mTime = temp.mTime });
        }
    }
}
