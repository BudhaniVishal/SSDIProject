using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.LogicLayer
{
	public class MockVerifyEmailLL
	{
		public ResultCode VerifyEmail(VerifyEmailModel model)
		{

		    DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			VerifyEmailDLLModel obj = new VerifyEmailDLLModel();
			obj.Email = model.Email;

			return objDatabaseAccess.VerifyEmail(obj);
		}
	}
}