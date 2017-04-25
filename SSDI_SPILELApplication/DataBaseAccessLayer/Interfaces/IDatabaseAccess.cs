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
		ResultCode UpdatePassword(UpdatePasswordModelDLL data, string email);
        bool SaveContributionForStory(ContributorStoryModel model);
    }
}
