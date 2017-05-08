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
	
	public class TestUpdatepasswordDataBaseAccess
	{
		[Test]
		public void TestUpdatePasswordPositive()
		{
			ResultCode result = new ResultCode();
			UpdatePasswordModelDLL obj = new UpdatePasswordModelDLL();
			obj.Password = "abcd123";
			obj.ConfirmPassword = "abcd123";
			result = new DatabaseAccess().UpdatePassword(obj, "vbudhani@uncc.edu");
			Assert.AreEqual(result.Message, "Password Updated Successfully !!");
		}
		[Test]
		public void TestUpdatePasswordNegative()
		{
			ResultCode result = new ResultCode();
			UpdatePasswordModelDLL obj = new UpdatePasswordModelDLL();
			obj.Password = "abcd123";
			obj.ConfirmPassword = "abcdef123";
			result = new DatabaseAccess().UpdatePassword(obj, "vbudhani@uncc.edu");
			Assert.AreEqual(result.Message, "Password and Confirm Password does not match !!");
		}
		

	}
}
