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
            obj.EmailAddress = "vishal@uncc.edu";
            obj.Password = "123456";
            obj.UserType = "WRITER";
            result = new MockDataBaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Writer Login Successful !!");
        }

        [Test]
        public void TestLoginWriterWithInValidPassword()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "vishal@uncc.edu";
            obj.Password = "14";
            obj.UserType = "WRITER";
            result = new MockDataBaseAccess().LoginUser(obj);
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
            result = new MockDataBaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Invalid Email Address");
        }

        [Test]
        public void TestLoginEditorwithValidDetailsAndApproved()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.True;
            obj.EmailAddress = "editor@uncc.edu";
            obj.Password = "123456";
            obj.UserType = "EDITOR";
            result = new MockDataBaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Editor Login Successful !!");
        }

        [Test]
        public void TestLoginEditorwithValidDetailsAndNotApproved()
        {
            ResultCode result = new ResultCode();
            UserRegistrationModel obj = new UserRegistrationModel();
            obj.IsUserVerified = BsonBoolean.False;
            obj.EmailAddress = "edit@uncc.edu";
            obj.Password = "123456";
            obj.UserType = "EDITOR";
            result = new MockDataBaseAccess().LoginUser(obj);
            Assert.AreEqual(result.Message, "Editor not verified yet !!");
        }
    }
}
