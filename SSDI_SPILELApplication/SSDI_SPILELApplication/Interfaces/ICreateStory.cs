﻿using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Interfaces
{
    interface ICreateStory
    {
        ResultCode CreateEditorStory(StoryModel story, string username);
        bool SaveContributionForStory(ContributeStoryModel model);
    }
}
