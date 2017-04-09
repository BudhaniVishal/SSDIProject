using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.LogicLayer;
using UserRegistrationModel = SSDI_SPILELApplication.Models.UserRegistrationModel;
using SSDI_SPILELApplication.Utilities;

namespace SSDI_SPILELApplication.Tests.MockClasses
{
	public class MockHomeController : Controller
	{

		[HttpPost]
		public JsonResult VerifyEmail(VerifyEmailModel v)
		{
			if (v != null)
			{
				
				MockVerifyEmailLL obj = new MockVerifyEmailLL();
				ResultCode result = obj.VerifyEmail(v);

				return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}
		[HttpPost]
		public JsonResult updatepassword(UpdatepasswordModel v,string e )
		{
			if (v != null)
			{
				if (!v.Password.Equals(v.ConfirmPassword))
				{
					return Json("Password and Confirm Password doesn't match !!");
				}
				String email = e;
				UpdatePasswordLL obj = new UpdatePasswordLL();
				ResultCode result = obj.updatepassword(v, email);

				return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}

	}
}
