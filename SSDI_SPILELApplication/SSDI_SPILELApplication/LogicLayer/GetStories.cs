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
    }
}