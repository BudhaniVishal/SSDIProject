using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Interfaces;
using DBLayer = DataBaseAccessLayer.ConnectionClass;

namespace SSDI_SPILELApplication.Models
{
    public class VerifyLogin : ILoginUser
    {
        public string LoginUser(LoginModel model)
        {
            string dataBaseName = ConfigurationManager.AppSettings["Database"];
            DBLayer.UserRegistrationModel obj = new DBLayer.UserRegistrationModel();
            obj.EmailAddress = model.Email;
            obj.Password = model.Password;
            return DataBaseAccessLayer.DatabaseAccess.LoginUser(obj, dataBaseName);
        }
    }
}