using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Bson;
using NUnit.Framework;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class LoginUserDataBaseAccessTest
    {
        [Test]
        public void TestLoginWriterWithValidCredentials()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = true;
            obj.EmailAddress = "mdeshpa3@gmail.com";
            obj.Password = "123456";
            obj.UserType = "WRITER";
            result = new DatabaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Writer Login Successful !!");
        }

        [Test]
        public void TestLoginWriterWithInValidPassword()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "vbudhani@uncc.edu";
            obj.Password = "14";
            obj.UserType = "WRITER";
            result = new DatabaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Incorrect Password !!");
        }

        [Test]
        public void TestLoginUserWithInValidEmail()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "aa@uncc.edu";
            obj.Password = "1234";
            obj.UserType = "WRITER";
            result = new DatabaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Invalid Email Address");
        }
    }
}
