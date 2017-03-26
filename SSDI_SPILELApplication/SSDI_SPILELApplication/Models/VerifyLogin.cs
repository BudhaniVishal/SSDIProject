using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Interfaces;

namespace SSDI_SPILELApplication.Models
{
    public class VerifyLogin : ILoginUser
    {
        public string LoginUser(LoginModel model)
        {
            LoginCheckDLLModel logindll = new LoginCheckDLLModel();
            logindll.Email = model.Email;
            logindll.Password = model.Password;
            return DataBaseAccessLayer.DatabaseAccess.LoginUser(logindll);
        }
    }
}