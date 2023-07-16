using CloudinaryDotNet.Actions;
using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using DoyleDispatchWebApp.Repositories;
using DoyleDispatchWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;
        private readonly IPhoto _photo;

        public DashboardController(IDashboard dashboard, IPhoto photo)
        {
            _dashboard = dashboard;
            _photo = photo;
        }
        private void MapUserEdit(Client user, EditClientViewModel editVM, ImageUploadResult photoResult) 
        {
            user.Id = editVM.Id;
            user.Surname = editVM.Surname;
            user.Othernames = editVM.Othernames;
            user.Gender = editVM.Gender;
            user.Birthdate = editVM.Birthdate;
            user.Address = editVM.Address;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.PhoneNumber = editVM.PhoneNumber;
        }
        public async Task<IActionResult> Index()
        {
            var userPackages = await _dashboard.GetAllUserPackages();
            var allusersPackages = await _dashboard.GetAllPackages();
            var dashboardViewModel = new DashboardViewModel
            {
                Packages = userPackages,
                packages = allusersPackages
            };
            return View(dashboardViewModel);
        }
        public async Task<IActionResult> EditClientProfile()
        {
            var curUserId = HttpContext.User.GetUserId();
            var user = await _dashboard.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editClientViewModel = new EditClientViewModel()
            {
                Id = curUserId,
                Surname = user.Surname,
                Othernames = user.Othernames,
                Gender = user.Gender,
                Birthdate = user.Birthdate,
                Address = user.Address,
                ProfileImageUrl = user.ProfileImageUrl,
                PhoneNumber = user.PhoneNumber
            };
            return View(editClientViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditClientProfile(EditClientViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit");
                return View("EditClientProfille");
            }
            Client user = await _dashboard.GetByIdNoTracking(editVM.Id);
            if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photo.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _dashboard.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photo.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Photo Could Not be Deleted");
                    return View(editVM);
                }
                var photoResult = await _photo.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _dashboard.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
