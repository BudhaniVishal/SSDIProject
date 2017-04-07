using System.Collections.Generic;
using DataBaseAccessLayer.ConnectionClass;
using System.Collections.Generic;

namespace DataBaseAccessLayer.Interfaces
{
    interface IDatabaseAccess
    {
        ResultCode CreateStory(ConnStoryTable story);
        ResultCode LoginUser(UserRegistrationModel user);
        ResultCode RegisterUser(UserRegistrationModel modelData);
        List<ConnStoryTable> GetAllStories();
        //List<ConnStoryTable> BrowseCreatorStory(string ID);
    }
}
