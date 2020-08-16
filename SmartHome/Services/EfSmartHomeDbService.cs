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

            if (dbContext.client.Where(c => c.login == clientSignUp.login).Any()){
                client = null;
            }else {
                Client cTmp = dbContext.client.FirstOrDefault();
            
                if (cTmp != null){
                    Console.WriteLine(cTmp.id_client);
                    var maxId = dbContext.client.Max(c => c.id_client);
                    client.id_client = maxId + 1;
                }
                else {
                    Console.WriteLine("baza jest pusta");
                    client.id_client = 0;
                }
                var salt = CreateSalt();
                var password = Create(clientSignUp.password, salt);
                client.login = clientSignUp.login;
                client.email = clientSignUp.email;
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
            if (dbContext.client.Where(c => c.login == signInRequest.login).Any()) {
                client = dbContext.client.SingleOrDefault(c => c.login == signInRequest.login);
                if (!Validate(signInRequest.password, client.salt, client.password)){
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

            var ListRooms = dbContext.room.Where(r => r.id_client == client.id_client).
                Select(room => new Room { id_room = room.id_room, id_client = room.id_client, name = room.name }).ToList();

            Console.WriteLine("Ef get account 2");

            var ListRoomDevices = new List<RoomDevices>();

            foreach (Room room in ListRooms) {
                var RoomDevices = new RoomDevices();
                RoomDevices.listParticulatesSensors = new List<ParticulatesSensor>();
                RoomDevices.listTemperatureSensors = new List<TemperatureSensor>();
                RoomDevices.Room = room;
                var ListDevices = dbContext.device.Where(d => d.id_room == room.id_room).
                    Select(device => new Device (device.id_device,device.id_room,device.name,device.type )).ToList();
                
                foreach (Device device in ListDevices) {
                    if (device.type == DeviceTypes.TemperatureSensor.ToString()){
                        var ListMeasurements = dbContext.temperature.Where(t => t.id_device == device.id_device).
                            Select(temp => new Temperature { id_temperature = temp.id_temperature, id_device = temp.id_device, t_value = temp.t_value, m_date = temp.m_date, m_time = temp.m_time }).ToList();
                        var temperatureSensor = new TemperatureSensor(device, ListMeasurements);
                        RoomDevices.listTemperatureSensors.Add(temperatureSensor);
                    }
                    else {
                        var ListMeasurements = dbContext.particulates.Where(p => p.id_device == device.id_device).
                            Select(part => new Particulates { id_particulates = part.id_particulates, id_device = part.id_device, pm25_value = part.pm25_value,pm10_value = part.pm10_value ,date = part.date, time = part.time }).ToList();
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
            return dbContext.temperature.Select(temp => new Temperature { id_temperature = temp.id_temperature, id_device = temp.id_device, t_value = temp.t_value, m_date = temp.m_date, m_time = temp.m_time });
        }
    }
}
