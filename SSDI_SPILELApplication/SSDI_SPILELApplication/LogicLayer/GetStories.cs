using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer;
using SSDI_SPILELApplication.Interfaces;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class GetStories : IGetStories
    {
        public List<StoryModel> GetAllStories()
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            var stories = objDatabaseAccess.GetAllStories();
            List<StoryModel> list = new List<StoryModel>();
            foreach(var item in stories)
            {
                StoryModel obj = new StoryModel();
                obj.Content = item.Content;
                obj.From = Convert.ToDateTime(item.From);
                obj.Genre = item.Genre;
                obj.Scenario = item.Scenario;
                obj.StoryID = Convert.ToInt32(item.StoryID);
                obj.Title = item.Title;
                obj.To = Convert.ToDateTime(item.To);
                obj.Type = item.Type;
                list.Add(obj);
            }
            return list;
        }

		

            public List<StoryModel> getCreatedStories(string username)
            {
                List<StoryModel> storyObj = new List<StoryModel>();
                DatabaseAccess objDatabaseAccess = new DatabaseAccess();
                var result = objDatabaseAccess.BrowseCreatedStories(username);
                foreach (var story in result)
                {
                    StoryModel Obj = new StoryModel();
                    Obj.Content = story.Content;
                    //Obj.From = story.From;
                    Obj.Genre = story.Genre;
                    Obj.Scenario = story.Scenario;
                    Obj.StoryID = Convert.ToInt32(story.StoryID);
                    Obj.Title = story.Title;
                    Obj.Type = story.Type;
                    storyObj.Add(Obj);
                }
                return storyObj;

            }
        public List<StoryModel> getContributorStories(string username)
        {
            List<StoryModel> storyObj = new List<StoryModel>();
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            var result = objDatabaseAccess.BrowseContributorStories(username);
            foreach (var story in result)
            {
                StoryModel Obj = new StoryModel();
                Obj.Content = story.Content;
                Obj.StoryID = Convert.ToInt32(story.StoryID);
                Obj.Title = story.Title;
                storyObj.Add(Obj);
            }
            return storyObj;

        }
        public List<StoryModel> BrowseContributedStoriesForEditor(int storyID)
        {
            List<StoryModel> storyObj = new List<StoryModel>();
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            var result = objDatabaseAccess.BrowseContributedStoriesForEditor(storyID);
            foreach (var story in result)
            {
                StoryModel Obj = new StoryModel();
                Obj.Content = story.Content;
                Obj.StoryID = Convert.ToInt32(story.StoryID);
                Obj.Title = story.Title;
                Obj.ContributorID = story.ContributorID;
                storyObj.Add(Obj);
            }
            return storyObj;

        }
        

        public StoryModel GetStoryByID(int id) {
			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			var item = objDatabaseAccess.GetStoryByID(id);
			StoryModel obj = new StoryModel();
			obj.Content = item.Content;
			obj.From = Convert.ToDateTime(item.From);
			obj.Genre = item.Genre;
			obj.Scenario = item.Scenario;
			obj.StoryID = Convert.ToInt32(item.StoryID);
			obj.Title = item.Title;
			obj.To = Convert.ToDateTime(item.To);
			obj.Type = item.Type;
			return obj;
		}
	}

    
}