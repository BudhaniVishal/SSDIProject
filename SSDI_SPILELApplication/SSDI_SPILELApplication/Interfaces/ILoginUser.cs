using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Interfaces
{
    interface ILoginUser
    {
        ResultCode LoginUser(LoginModel model);
    }
}
