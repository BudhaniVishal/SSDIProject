using DataBaseAccessLayer.ConnectionClass;

namespace DataBaseAccessLayer.Interfaces
{
    interface IDatabaseAccess
    {
        ResultCode CreateStory(ConnStoryTable story);
        ResultCode LoginUser(UserRegistrationModel user);
        ResultCode RegisterUser(UserRegistrationModel modelData);
		ResultCode VerifyEmail(VerifyEmailDLLModel email);
		ResultCode updatepassword(UpdatePasswordModelDLL data, string email);
	}
}
