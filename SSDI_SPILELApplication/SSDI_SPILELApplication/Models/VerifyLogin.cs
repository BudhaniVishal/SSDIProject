using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;

namespace SSDI_SPILELApplication.Models
{
    public class VerifyLogin
    {
        public bool verifyuser(LoginModel l)
        {
           

            LoginCheckDLLModel logindll = new LoginCheckDLLModel();
            logindll.Email = l.Email;
            logindll.Password = l.Password;

            var x =  DataBaseAccessLayer.DatabaseAccess.CheckUserExists(logindll);

            return x;



        }
    }
}