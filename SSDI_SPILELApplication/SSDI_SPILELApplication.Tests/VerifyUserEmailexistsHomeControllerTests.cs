using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.LogicLayer;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Tests.MockClasses;

namespace SSDI_SPILELApplication.Tests
{
	public class VerifyUserEmailexistsHomeControllerTests
	{
		private readonly string errorMessage = "Error !! Data is null.";
		[Test]
		public void VerifyUserEmailExistsForNullValues()
		{
			MockHomeController obj = new MockHomeController();
			JsonResult result = obj.VerifyEmail(null);
			Assert.AreEqual(result.Data.ToString(), errorMessage);
		}

		[Test]
		public void VerifyUserEmailExistsForCorrectValues()
		{
			MockHomeController obj = new MockHomeController();
			VerifyEmailModel model = new VerifyEmailModel();
			model.Email = "vbudhani@uncc.edu";
		
			JsonResult result = obj.VerifyEmail(model);
			Assert.AreEqual(result.Data.ToString(), "Registered User !!");
		}
		[Test]
		public void VerifyUserEmailExistsForNegativeCase()
		{
			MockHomeController obj = new MockHomeController();
			VerifyEmailModel model = new VerifyEmailModel();
			model.Email = "vbudh@uncc.edu";
			JsonResult result = obj.VerifyEmail(model);
			Assert.AreEqual(result.Data.ToString(), "User does not exists !!");
		}

	}
}
