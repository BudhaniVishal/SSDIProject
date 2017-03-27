using DataBaseAccessLayer.ConnectionClass;

namespace DataBaseAccessLayer.Interfaces
{
    interface IDatabaseAccess
    {
        bool CreateStory(ConnStoryTable story);
        string LoginUser(UserRegistrationModel user);
        bool RegisterUser(UserRegistrationModel modelData);
    }
}
