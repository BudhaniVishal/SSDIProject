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
	public class UpdatePasswordLL
	{
		public ResultCode UpdatePassword(UpdatepasswordModel model, string email)
		{

			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			DataBaseAccessLayer.ConnectionClass.UpdatePasswordModelDLL obj = new DataBaseAccessLayer.ConnectionClass.UpdatePasswordModelDLL();
			obj.Password = model.Password;
			obj.ConfirmPassword = model.ConfirmPassword;
			string emailLL = email;
			

			return objDatabaseAccess.UpdatePassword(obj,emailLL);
		}
	}
}