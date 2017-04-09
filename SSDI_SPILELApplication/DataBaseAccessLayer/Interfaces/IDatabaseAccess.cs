using System.Collections.Generic;
using DataBaseAccessLayer.ConnectionClass;

namespace DataBaseAccessLayer.Interfaces
{
    interface IDatabaseAccess
    {
        ResultCode CreateStory(ConnStoryTable story, string username);
        ResultCode LoginUser(UserRegistrationModel user);
        ResultCode RegisterUser(UserRegistrationModel modelData);
        List<ConnStoryTable> GetAllStories();
		ResultCode VerifyEmail(VerifyEmailDLLModel email);
		ResultCode updatepassword(UpdatePasswordModelDLL data, string email);
	}
}
