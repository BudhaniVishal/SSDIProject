using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class VerifyLogin : ILoginUser
    {
        public ResultCode LoginUser(LoginModel model)
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            DataBaseAccessLayer.ConnectionClass.UserRegistrationModel obj = new DataBaseAccessLayer.ConnectionClass.UserRegistrationModel();
            obj.EmailAddress = model.Email;
            obj.Password = model.Password;
            return objDatabaseAccess.LoginUser(obj);
        }
    }
}