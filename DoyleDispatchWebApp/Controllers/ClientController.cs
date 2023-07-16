using DoyleDispatchWebApp.Repositories;
using DoyleDispatchWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClient _client;

        public ClientController(IClient client)
        {
            _client = client;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _client.GetAllClients();
            List<ClientViewModel> result = new List<ClientViewModel>();
            foreach(var user in users)
            {
                var clientViewModel = new ClientViewModel()
                {
                    Id = user.Id,
                    Username = user.Email,
                    Surname = user.Surname,
                    Othernames = user.Othernames,
                    Gender = user.Gender,
                    Birthdate = user.Birthdate,
                    Address = user.Address,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                result.Add(clientViewModel);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _client.GetClientById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Client");
            }
            var detailViewModel = new DetailViewModel()
            {
                Id = user.Id,
                Username = user.Email,
                Surname = user.Surname,
                Othernames = user.Othernames,
                Gender = user.Gender,
                Birthdate = user.Birthdate,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(detailViewModel);
        }
    }
}
