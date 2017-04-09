using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.LogicLayer
{
	public class MockVerifyEmailLL
	{
		public ResultCode VerifyEmail(VerifyEmailModel model)
		{

			MockDataBaseAccess objDatabaseAccess = new MockDataBaseAccess();
			DataBaseAccessLayer.ConnectionClass.VerifyEmailDLLModel obj = new DataBaseAccessLayer.ConnectionClass.VerifyEmailDLLModel();
			obj.Email = model.Email;

			return objDatabaseAccess.VerifyEmail(obj);
		}
	}
}