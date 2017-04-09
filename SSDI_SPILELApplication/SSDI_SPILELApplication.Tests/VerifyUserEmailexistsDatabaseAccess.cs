using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Bson;
using NUnit.Framework;

namespace SSDI_SPILELApplication.Tests
{
	class VerifyUserEmailexistsDatabaseAccess
	{
		[Test]
		public void TestUserEmailExistsPositive()
		{
			ResultCode result = new ResultCode();
			VerifyEmailDLLModel obj = new VerifyEmailDLLModel();
			obj.Email = "vbudhani@uncc.edu";
			result = new MockDataBaseAccess().VerifyEmail(obj);
			Assert.AreEqual(result.Message, "Registered User !!");
		}

		[Test]
		public void TestUserEmailExistsNegative()
		{
			ResultCode result = new ResultCode();
			VerifyEmailDLLModel obj = new VerifyEmailDLLModel();
			obj.Email = "vishal@uncc.edu";
			result = new MockDataBaseAccess().VerifyEmail(obj);
			Assert.AreEqual(result.Message, "User does not exists !!");
		}
		[Test]
		public void TestUserEmailExistsEmptyString()
		{
			ResultCode result = new ResultCode();
			VerifyEmailDLLModel obj = new VerifyEmailDLLModel();
			obj.Email =String.Empty;
			result = new MockDataBaseAccess().VerifyEmail(obj);
			Assert.AreEqual(result.Message, "User does not exists !!");
		}

	}
}
