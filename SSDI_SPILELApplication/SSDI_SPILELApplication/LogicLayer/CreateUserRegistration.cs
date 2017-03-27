using DataBaseAccessLayer;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;
using DBLayer = DataBaseAccessLayer.ConnectionClass;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class CreateUserRegistration : IRegistration
    {
        public bool RegisterUser(UserRegistrationModel modelData)
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            DBLayer.UserRegistrationModel obj = new DBLayer.UserRegistrationModel
            {
                FirstName = modelData.FirstName,
                LastName = modelData.LastName,
                EmailAddress = modelData.EmailAddress,
                Password = modelData.Password,
                ConfirmPassword = modelData.ConfirmPassword,
                UserType = modelData.UserType.ToString()
            };

            return objDatabaseAccess.RegisterUser(obj); 
        }
    }
}