using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;

namespace SSDI_SPILELApplication.Models
{
    public class CreateStory : ICreateStory
    {
        public bool CreateEditorStory(StoryModel story)
        {
            var result = false;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Content = story.Content;
            obj.from = story.from;
            obj.Scenario = story.Scenario;
            obj.StoryID = story.StoryID;
            obj.Title = story.Title;
            obj.to = story.to;
            obj.Type = story.Type;
            obj.genre = story.genre;

            result=DataBaseAccessLayer.DatabaseAccess.CreateStory(obj);

            return result;
        }
    }
}