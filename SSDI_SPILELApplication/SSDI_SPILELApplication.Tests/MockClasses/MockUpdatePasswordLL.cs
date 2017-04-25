using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Models;
using DataBaseAccessLayer.ConnectionClass;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.LogicLayer
{
	public class MockUpdatePasswordLL
	{
		public ResultCode UpdatePassword(UpdatepasswordModel model, string email)
		{

			MockDataBaseAccess objDatabaseAccess = new MockDataBaseAccess();
			DataBaseAccessLayer.ConnectionClass.UpdatePasswordModelDLL obj = new DataBaseAccessLayer.ConnectionClass.UpdatePasswordModelDLL();
			obj.Password = model.Password;
			obj.ConfirmPassword = model.ConfirmPassword;
			string emailLL = email;


			return objDatabaseAccess.UpdatePassword(obj, emailLL);
		}
	}
}