using System;
using System.Globalization;
using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using NUnit.Framework;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class RegisterUserDataBaseAccessTest
    {
        private string dataBaseName = "spielDBTest";
        [Test]
        public void TestRegisterUserWithExistingEmail()
        {
            bool result = true;
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.FirstName = "Test";
            obj.LastName = "Test";
            obj.Password = "Test";
            obj.ConfirmPassword = "Test";
            obj.UserType = "WRITER";
            obj.IsUserVerified = obj.UserType.Equals("WRITER");
            obj.EmailAddress = "vbudhani@uncc.edu"; // email id exists
            result = DatabaseAccess.RegisterUser(obj, dataBaseName);
            Assert.IsFalse(result); // value exists

        }
        [Test]
        public void TestRegisterUserWithNewEmail()
        {
            bool result = false;
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.FirstName = "Test";
            obj.LastName = "Test";
            obj.Password = "Test";
            obj.ConfirmPassword = "Test";
            obj.UserType = "WRITER";
            obj.IsUserVerified = obj.UserType.Equals("WRITER");
            obj.EmailAddress = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ","") +"test@uncc.edu"; // new email id
            result = DatabaseAccess.RegisterUser(obj, dataBaseName);
            Assert.IsTrue(result); // value exists

        }
    }
}
