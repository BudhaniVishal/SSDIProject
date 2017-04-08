using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.Models
{
	public class UpdatepasswordModel
	{
		private string password;
		private string confirmPassword;
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				return confirmPassword;
			}
			set
			{
				confirmPassword = value;
			}
		}
	}
}