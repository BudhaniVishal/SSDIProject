using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Interfaces;
using DataBaseAccessLayer;

namespace SSDI_SPILELApplication.LogicLayer
{
    //public class BrowseCreatorStory : IBrowseCreatorStory
    //{
    //        public List<ConnStoryTable> BrowseStories(string ID)
    //        {
    //            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
    //            List<ConnStoryTable> result = new List<ConnStoryTable>();
    //            List<StoryModel> storyObj = new List<StoryModel>(); 
               
    //            result = objDatabaseAccess.BrowseCreatorStory(ID);
    //        for(int i=0;i<result.Count;i++) {
                

    //        }
    //        //obj.Content = story.Content;
    //        //obj.From = Convert.ToString(story.From, CultureInfo.InvariantCulture);
    //        //obj.Scenario = story.Scenario;
    //        //obj.StoryID = story.StoryID;
    //        //obj.Title = story.Title;
    //        //obj.To = Convert.ToString(story.To, CultureInfo.InvariantCulture);
    //        //obj.Type = story.Type;
    //        //obj.Genre = story.Genre;

    //        return result;
    //        }
    //}
}