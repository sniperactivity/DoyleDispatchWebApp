using DoyleDispatchWebApp.Controllers;
using DoyleDispatchWebApp.Models;
using DoyleDispatchWebApp.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoyleDispatchWebApp.Tests.Controller
{
    public class PackageControllerTests
    {
        private PackageController _packageController; 
        private IPackage _package;
        private IPhoto _photo;
        public PackageControllerTests()
        {
            _package = A.Fake<IPackage>();
            _photo = A.Fake<IPhoto>();
            //sut
            _packageController = new PackageController(_package, _photo);
        }
        [Fact]
        public void PackageController_Index_ReturnSuccess()
        {
            //Arrange
            var packages = A.Fake<IEnumerable<Package>>();
            A.CallTo(() => _package.GetAll()).Returns(packages);
            //Act
            var result = _packageController.Index();
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void PackageController_Detail_ReturnSucces()
        {
            //Arrange
            var id = 1;
            var package = A.Fake<Package>();
            A.CallTo(() => _package.GetByIdAsync(id)).Returns(package);
            //Act
            var result = _packageController.Detail(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
