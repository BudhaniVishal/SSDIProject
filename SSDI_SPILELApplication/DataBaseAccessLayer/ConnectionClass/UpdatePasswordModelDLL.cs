using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAccessLayer.ConnectionClass
{
	public class UpdatePasswordModelDLL
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
