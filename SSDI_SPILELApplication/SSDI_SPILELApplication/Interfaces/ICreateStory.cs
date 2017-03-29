using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Interfaces
{
    interface ICreateStory
    {
        ResultCode CreateEditorStory(StoryModel story);
    }
}
