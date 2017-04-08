using System;
using System.Globalization;
using DataBaseAccessLayer.ConnectionClass;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;
using System.Collections.Generic;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class CreateStory : ICreateStory
    {
        public ResultCode CreateEditorStory(StoryModel story)
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            ConnStoryTable obj = new ConnStoryTable();
            obj.Content = story.Content;
            obj.From = Convert.ToString(story.From, CultureInfo.InvariantCulture);
            obj.Scenario = story.Scenario;
            obj.StoryID = story.StoryID;
            obj.Title = story.Title;
            obj.To = Convert.ToString(story.To, CultureInfo.InvariantCulture);
            obj.Type = story.Type;
            obj.Genre = story.Genre;

            return objDatabaseAccess.CreateStory(obj); ;
        }
    }
}