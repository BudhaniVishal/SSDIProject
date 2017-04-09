using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Tests
{
	public class VerifyUserEmailexistsHomeControllerTests
	{
		private readonly string errorMessage = "Error !! Data is null.";
		[Test]
		public void VerifyUserEmailExistsForNullValues()
		{
			HomeController obj = new HomeController();
			JsonResult result = obj.VerifyEmail(null);
			Assert.AreEqual(result.Data.ToString(), errorMessage);
		}

		[Test]
		public void VerifyUserEmailExistsForCorrectValues()
		{
			HomeController obj = new HomeController();
			VerifyEmailModel model = new VerifyEmailModel();
			model.Email = "vbudhani@uncc.edu";
			JsonResult result = obj.VerifyEmail(model);
			Assert.AreEqual(result.Data.ToString(), "Registered User !!");
		}
		[Test]
		public void VerifyUserEmailExistsForNegativeCase()
		{
			HomeController obj = new HomeController();
			VerifyEmailModel model = new VerifyEmailModel();
			model.Email = "vbudh@uncc.edu";
			JsonResult result = obj.VerifyEmail(model);
			Assert.AreEqual(result.Data.ToString(), "User does not exists !!");
		}

	}
}
