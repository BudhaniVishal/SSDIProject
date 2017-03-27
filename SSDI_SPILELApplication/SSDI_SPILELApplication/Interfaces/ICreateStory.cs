using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Interfaces
{
    interface ICreateStory
    {
        bool CreateEditorStory(StoryModel story);
    }
}
