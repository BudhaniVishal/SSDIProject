using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Tests.MockClasses;

namespace SSDI_SPILELApplication.Tests
{
	public class TestUpdatePasswordHomeController
	{
		private readonly string errorMessage = "Error !! Data is null.";
		[Test]
		public void UpdatePasswordNullValues()
		{
			MockHomeController obj = new MockHomeController();
			JsonResult result = obj.UpdatePassword(null, "");
			Assert.AreEqual(result.Data.ToString(), errorMessage);
		}


		[Test]
		public void UpdatePasswordForInCorrectValues()
		{
			MockHomeController obj = new MockHomeController();
			UpdatepasswordModel model = new UpdatepasswordModel();
			model.Password = "testpassword";
			model.ConfirmPassword = "testpassword124";
			string s = "vbudhani@uncc.edu";
			JsonResult result = obj.UpdatePassword(model, s);
			Assert.AreEqual(result.Data.ToString(), "Password and Confirm Password doesn't match !!");
		}
	}
}
