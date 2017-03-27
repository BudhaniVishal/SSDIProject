using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;
using System.Threading.Tasks;

namespace SSDI_SPILELApplication.Models
{
    public class CreateStory : ICreateStory
    {
        public bool CreateEditorStory(StoryModel story)
        {
            string dataBaseName = ConfigurationManager.AppSettings["Database"];
            ConnStoryTable obj = new ConnStoryTable();
            obj.Content = story.Content;
            obj.From = Convert.ToString(story.from, CultureInfo.InvariantCulture);
            obj.Scenario = story.Scenario;
            obj.StoryID = story.StoryID;
            obj.Title = story.Title;
            obj.To = Convert.ToString(story.to, CultureInfo.InvariantCulture);
            obj.Type = story.Type;
            obj.Genre = story.genre;
            
            return DataBaseAccessLayer.DatabaseAccess.CreateStory(obj, dataBaseName); ;
        }
    }
}