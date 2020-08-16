using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using SmartHome.Request;
using SmartHome.Services;

namespace SmartHome.Controllers{
    public class SmartHomeController : Controller{
        private readonly ISmartHomeDbService dbService;

        public SmartHomeController(ISmartHomeDbService dbService){
            this.dbService = dbService;
        }

        public IActionResult Index(){
            return View();
        }

        //[Authorize]
        public IActionResult Account() {
            Console.WriteLine("Smart home account");
            return View();
        }

        public IActionResult SignIn(SignInRequest signInRequest) {
            if (ModelState.IsValid){
                Client client = dbService.Login(signInRequest);
                if (client != null) {
                    Account account = dbService.GetAccount(client);
                    return View("Account",account);
                }

                return View(signInRequest);
            }
            return View(signInRequest);
        }

        public IActionResult SignUp(ClientSignUp clientSignUp) {
            Client client = new Client();
            if (ModelState.IsValid) {
                client = dbService.Register(clientSignUp);
                if (client != null) {
                    return RedirectToAction("SignIn");
                }
            }else {
                return View(clientSignUp);
            }
            return View();
        }

        public IActionResult ManageDevices() {
            return View();
        }

        public IActionResult Temperature() {
            return View("Temperature",dbService.GetTemperature());
        }
    }
}