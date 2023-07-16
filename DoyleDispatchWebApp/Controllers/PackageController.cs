using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using DoyleDispatchWebApp.Repositories;
using DoyleDispatchWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackage _package;
        private readonly IPhoto _photo;
        //private readonly HttpContextAccessor _httpContextAccessor;

        public PackageController(IPackage package, IPhoto photo /* HttpContextAccessor httpContextAccessor*/)
        {
            _package = package;
            _photo = photo;
            //_httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Package> packages = await _package.GetAll();
            return View(packages);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Package packages = await _package.GetByIdAsync(id);
            return View(packages);
        }
        public IActionResult Create()
        {
            var curUserId = /*_httpContextAccessor.*/HttpContext.User.GetUserId();
            var createViewModel = new CreateViewModel { ClientId = curUserId };
            return View(createViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel packagevm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photo.AddPhotoAsync(packagevm.Image);

                var package = new Package 
                {
                    PackageName = packagevm.PackageName,
                    DropDate = packagevm.DropDate,
                    Image = result.Url.ToString(),
                    PackageCategory = packagevm.PackageCategory,
                    ClientId = packagevm.ClientId,
                    Destination = new Destination
                    {
                        Name = packagevm.Destination.Name,
                        State = packagevm.Destination.State,
                        Address = packagevm.Destination.Address,
                        Contact = packagevm.Destination.Contact
                    }
                };
                _package.Add(package);
                return RedirectToAction("Index","Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo Failed To Upload");
            }
            return View(packagevm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var package = await _package.GetByIdAsync(id);
            if (package == null) return View("Error");
            var packagevm = new EditViewModel
            {
                PackageName = package.PackageName,
                DropDate=package.DropDate,
                DestinationId =package.DestinationId,
                Destination = package.Destination,
                ClientId = package.ClientId,
                URL = package.Image,
                PackageCategory=package.PackageCategory
            };
            return View(packagevm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel packagevm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Delivery Information");
                return View("Edit", packagevm);
            }

            var userpackage = await _package.GetByIdAsyncNoTracking(id);
            if (userpackage == null)
            {
                return View("Error");
            }

            var photoresult = await _photo.AddPhotoAsync(packagevm.Image);
            if(photoresult.Error != null)
            {
                ModelState.AddModelError("", "Could Not Delete Photo");
                return View(packagevm);
            }
            if (!string.IsNullOrEmpty(userpackage.Image))
            {
                _ = _photo.DeletePhotoAsync(userpackage.Image);
            }

            var package = new Package
            {
                Id = id,
                PackageName = packagevm.PackageName,
                DropDate = packagevm.DropDate,
                Image = photoresult.Url.ToString(),
                DestinationId = packagevm.DestinationId,
                Destination = packagevm.Destination,
                ClientId = packagevm.ClientId,               
            };
            _package.Update(package);
            return RedirectToAction("Index", "Dashboard");
            //{
            //    try
            //    {
            //        await _photo.DeletePhotoAsync(userpackage.Image);

            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError("", "Could Not Delete Photo");
            //        return View(packagevm);
            //    }
            //    var photoresult = await _photo.AddPhotoAsync(packagevm.Image);
            //    var package = new Package
            //    {
            //        Id = id,
            //        PackageName = packagevm.PackageName,
            //        DropDate = packagevm.DropDate,
            //        Image = photoresult.Url.ToString(),
            //        DestinationId = packagevm.DestinationId,
            //        Destination = packagevm.Destination,
            //    };
            //    _package.Update(package);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View(packagevm);
            //}
        }
        public async Task<IActionResult> Delete(int id)
        {
            var packagedetails = await _package.GetByIdAsync(id);
            if (packagedetails == null) return View("ERROR");
            return View(packagedetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePackage(int id) 
        {
            var packagedetails = await _package.GetByIdAsync(id);
            if (packagedetails == null) 
            { 
                return View("ERROR"); 
            }
            if (!string.IsNullOrEmpty(packagedetails.Image))
            {
                _ = _photo.DeletePhotoAsync(packagedetails.Image);
            }
            _package.Delete(packagedetails);
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> PackageApproval(int id)
        {
            var query = await _package.GetByIdAsyncNoTracking(id);          
            return View(query);
        }
        [HttpPost]
        public IActionResult PackageApproval(Package p)
        {            
            Package inv = new Package()
            {
                Id = p.Id,
                PackageName = p.PackageName,
                DropDate = p.DropDate,
                Image = p.Image,
                PackageCategory = p.PackageCategory,
                DestinationId = p.DestinationId,
                Destination = p.Destination,
                ClientId = p.ClientId,
                Status = p.Status
            };
            _package.Update(inv);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}