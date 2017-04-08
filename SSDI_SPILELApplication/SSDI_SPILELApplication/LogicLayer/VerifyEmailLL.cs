using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Models;
using DataBaseAccessLayer.ConnectionClass;


namespace SSDI_SPILELApplication.LogicLayer
{
	public class VerifyEmailLL 
	{
		public ResultCode VerifyEmail(VerifyEmailModel model)
		{
		
			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			DataBaseAccessLayer.ConnectionClass.VerifyEmailDLLModel obj = new DataBaseAccessLayer.ConnectionClass.VerifyEmailDLLModel();
			obj.Email = model.Email;

			return objDatabaseAccess.VerifyEmail(obj);
		}
	}
}