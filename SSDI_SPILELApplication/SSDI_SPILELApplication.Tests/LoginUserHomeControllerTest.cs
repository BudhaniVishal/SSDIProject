using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class LoginUserHomeControllerTest
    {
        private readonly string errorMessage = "Error !! Data is null.";
        [Test]
        public void LoginTestForNullValues()
        {
            HomeController obj = new HomeController();
            JsonResult result = obj.Login(null);
            Assert.AreEqual(result.Data.ToString(), errorMessage);
        }

        [Test]
        public void LoginTestForCorrectValues()
        {
            HomeController obj = new HomeController();
            LoginModel model = new LoginModel();
            model.Email = "vbudhani@uncc.edu";
            model.Password = "1234";
            JsonResult result = obj.Login(model);
            Assert.AreNotEqual(result.Data.ToString(), errorMessage);
        }
    }
}
