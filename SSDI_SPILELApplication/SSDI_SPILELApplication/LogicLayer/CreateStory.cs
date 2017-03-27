using System;
using System.Globalization;
using DataBaseAccessLayer.ConnectionClass;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class CreateStory : ICreateStory
    {
        public bool CreateEditorStory(StoryModel story)
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            ConnStoryTable obj = new ConnStoryTable();
            obj.Content = story.Content;
            obj.From = Convert.ToString(story.from, CultureInfo.InvariantCulture);
            obj.Scenario = story.Scenario;
            obj.StoryID = story.StoryID;
            obj.Title = story.Title;
            obj.To = Convert.ToString(story.to, CultureInfo.InvariantCulture);
            obj.Type = story.Type;
            obj.Genre = story.genre;

            return objDatabaseAccess.CreateStory(obj); ;
        }
    }
}