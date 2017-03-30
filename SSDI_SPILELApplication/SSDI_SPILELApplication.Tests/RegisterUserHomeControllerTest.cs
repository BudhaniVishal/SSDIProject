using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Tests
{
    public class RegisterUserHomeControllerTest
    {
        private readonly string errorMessage = "Error !! Data is null.";
        [Test]
        public void RegisterUserTestForNullValues()
        {
            HomeController obj = new HomeController();
            JsonResult result = obj.UserRegistration(null);
            Assert.AreEqual(result.Data.ToString(), errorMessage);
        }

        [Test]
        public void LoginTestForCorrectValues()
        {
            HomeController obj = new HomeController();
            UserRegistrationModel model = new UserRegistrationModel();
            model.FirstName = "Test User";
            model.LastName = "Test User";
            model.EmailAddress = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "")+"@m.com";
            model.Password = "Test Password";
            model.ConfirmPassword = "Test Password";
            model.UserType = UserType.WRITER;
            JsonResult result = obj.UserRegistration(model);
            Assert.AreNotEqual(result.Data.ToString(), errorMessage);
        }
    }
}
