using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoT.WebApp.Models;

namespace IoT.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _service;
        public HomeController(UserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DisplayUserDetails()
        {
            return View("UsersList", _service.GetAll());
        }

        public IActionResult Users()
        { 
            return View(_service.Get(3));
        }

        [HttpPost]
        public IActionResult CreateUser(User userToCreate)
        {
            if (_service.CreateAsync(userToCreate) == null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Modules()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
