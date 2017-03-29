using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer.ConnectionClass;
using UserRegistrationModel = SSDI_SPILELApplication.Models.UserRegistrationModel;

namespace SSDI_SPILELApplication.Interfaces
{
    public interface IRegistration
    {
        ResultCode RegisterUser(UserRegistrationModel userData);
    }
}
