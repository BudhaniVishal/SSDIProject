using System;
using System.Globalization;
using System.Threading;
using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Driver;
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
            result = new MockDataBaseAccess().RegisterUser(obj);
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
            result = new MockDataBaseAccess().RegisterUser(obj);
            Assert.IsTrue(result); // value added

        }

        [Test]
        public void TestRegisterUserWithNewEmailForDbInsert()
        {
            
            var condition = Builders<UserRegistrationModel>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<UserRegistrationModel>("UserRegistration");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            //Insert data
            bool result = false;
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.FirstName = "Test";
            obj.LastName = "Test";
            obj.Password = "Test";
            obj.ConfirmPassword = "Test";
            obj.UserType = "WRITER";
            obj.IsUserVerified = obj.UserType.Equals("WRITER");
            Thread.Sleep(1000); // Given for synchronization
            obj.EmailAddress = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "test@uncc.edu"; // new email id          
            result = new MockDataBaseAccess().RegisterUser(obj);
            Assert.IsTrue(result); // value added

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count+1, countNew); // Value added to database
        }

        [Test]
        public void TestRegisterUserWithExistingEmailForDbInsert()
        {
            var condition = Builders<UserRegistrationModel>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<UserRegistrationModel>("UserRegistration");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            //Insert data
            bool result = true;
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.FirstName = "Test";
            obj.LastName = "Test";
            obj.Password = "Test";
            obj.ConfirmPassword = "Test";
            obj.UserType = "WRITER";
            obj.IsUserVerified = obj.UserType.Equals("WRITER");
            obj.EmailAddress = "vbudhani@uncc.edu"; // email id exists
            result = new MockDataBaseAccess().RegisterUser(obj);
            Assert.IsFalse(result); // value exists

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count, countNew); // Value added to database
        }
    }
}
