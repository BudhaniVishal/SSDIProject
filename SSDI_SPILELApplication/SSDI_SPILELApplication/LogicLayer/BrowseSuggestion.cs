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
    public class BrowseSuggestion : IBrowseSuggestions
    {
        public List<SuggestionTable> BrowseSuggestions(int story_id)
        {
			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			List<SuggestionTable> result = new List<SuggestionTable>();
			List<SuggestionModel> suggestionObj = new List<SuggestionModel>(); 
				   
			result = objDatabaseAccess.BrowseSuggestions(story_id);
			for(int i=0;i<result.Count;i++) {
				suggestionObj[i].Content = result[i].Content;
				suggestionObj[i].DatePosted = result[i].DatePosted;
				suggestionObj[i].OwningStoryID = result[i].OwningStoryID;
				suggestionObj[i].SuggestionID = result[i].SuggestionID;
			}
			
			return result;
		}
	}
}