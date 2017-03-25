using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Interfaces
{
    public interface IRegistration
    {
        bool RegisterUser(string firstName, string lastName, string emailAddress, string password, string confirmPassword, UserType userType);
    }
}
