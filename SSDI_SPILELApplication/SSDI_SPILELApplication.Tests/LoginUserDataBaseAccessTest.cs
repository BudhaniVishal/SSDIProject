using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Bson;
using NUnit.Framework;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class LoginUserDataBaseAccessTest
    {
        private string dataBaseName = "spielDBTest";
        [Test]
        public void TestLoginWriterWithValidCredentials()
        {
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = true;
            obj.EmailAddress = "vishal@uncc.edu";
            obj.Password = "1234";
            obj.UserType = "WRITER";
            string s = DatabaseAccess.LoginUser(obj, dataBaseName);
            Assert.AreEqual(s, "Writer Login Successful !!");
        }

        [Test]
        public void TestLoginWriterWithInValidPassword()
        {
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "vishal@uncc.edu";
            obj.Password = "14";
            obj.UserType = "WRITER";
            string s = DatabaseAccess.LoginUser(obj, dataBaseName);
            Assert.AreEqual(s, "Incorrect Password !!");
        }

        [Test]
        public void TestLoginUserWithInValidEmail()
        {
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "aa@uncc.edu";
            obj.Password = "1234";
            obj.UserType = "WRITER";
            string s = DatabaseAccess.LoginUser(obj, dataBaseName);
            Assert.AreEqual(s, "Invalid Email Address");
        }

        [Test]
        public void TestLoginEditorwithValidDetailsAndApproved()
        {
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "editor@uncc.edu";
            obj.Password = "1234";
            obj.UserType = "EDITOR";
            string s = DatabaseAccess.LoginUser(obj, dataBaseName);
            Assert.AreEqual(s, "Editor Login Successful !!");
        }

        [Test]
        public void TestLoginEditorwithValidDetailsAndNotApproved()
        {
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.False;
            obj.EmailAddress = "edit@uncc.edu";
            obj.Password = "1234";
            obj.UserType = "EDITOR";
            string s = DatabaseAccess.LoginUser(obj,dataBaseName);
            Assert.AreEqual(s, "Editor not verified yet ");
        }
    }
}
