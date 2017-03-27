using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;
using DBLayer = DataBaseAccessLayer.ConnectionClass;
using System.Configuration;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class CreateUserRegistration : IRegistration
    {
        public bool RegisterUser(UserRegistrationModel modelData)
        {
            string dataBaseName = ConfigurationManager.AppSettings["Database"];
            DBLayer.UserRegistrationModel obj = new DBLayer.UserRegistrationModel
            {
                FirstName = modelData.FirstName,
                LastName = modelData.LastName,
                EmailAddress = modelData.EmailAddress,
                Password = modelData.Password,
                ConfirmPassword = modelData.ConfirmPassword,
                UserType = modelData.UserType.ToString()
            };

            return DatabaseAccess.RegisterUser(obj, dataBaseName); 
        }
    }
}