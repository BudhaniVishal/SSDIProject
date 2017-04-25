using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSDI_SPILELApplication.LogicLayer;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Utilities
{
	public class HomeControllerUtilities
	{
		public static List<SelectListItem> GetGenres()
		{
			List<SelectListItem> items = new List<SelectListItem>();
			items.Add(new SelectListItem
			{
				Text = "Action/Adventure",
				Value = "Action/Adventure"
			});
			items.Add(new SelectListItem
			{
				Text = "Business",
				Value = "Business"
			});
			items.Add(new SelectListItem
			{
				Text = "Career",
				Value = "Career"
			});
			items.Add(new SelectListItem
			{
				Text = "Comedy",
				Value = "Comedy",
			});
			items.Add(new SelectListItem
			{
				Text = "Detective",
				Value = "Detective"
			});
			items.Add(new SelectListItem
			{
				Text = "Family",
				Value = "Family"
			});
			items.Add(new SelectListItem
			{
				Text = "Ghost",
				Value = "Ghost"
			});
			items.Add(new SelectListItem
			{
				Text = "Mystery",
				Value = "Mystery",
			});
			items.Add(new SelectListItem
			{
				Text = "Thriller/Suspense",
				Value = "Thriller/Suspense"
			});
			return items;
		}

		public static List<SelectListItem> GetTypes()
		{
			List<SelectListItem> items = new List<SelectListItem>();
			items.Add(new SelectListItem
			{
				Text = "Short Stories",
				Value = "Short Stories"
			});
			items.Add(new SelectListItem
			{
				Text = "Article",
				Value = "Article"
			});
			items.Add(new SelectListItem
			{
				Text = "Poetry",
				Value = "Poetry"
			});
			items.Add(new SelectListItem
			{
				Text = "Fiction",
				Value = "Fiction"
			});
			items.Add(new SelectListItem
			{
				Text = "Non Fiction",
				Value = "Non Fiction"
			});
			items.Add(new SelectListItem
			{
				Text = "Classics",
				Value = "Classics"
			});
			return items;
		}

		public static List<StoryModel> FilterStories(List<StoryModel> storiesAvailable, string selectedGenre,
			string selectedType)
		{
			try
			{
				if (selectedGenre != string.Empty)
				{
					storiesAvailable = storiesAvailable.FindAll(x => x.Genre == selectedGenre).ToList();
				}
				if (selectedType != string.Empty)
				{
					storiesAvailable = storiesAvailable.FindAll(x => x.Type == selectedType).ToList();
				}
				return storiesAvailable;
			}

			catch (Exception ex)
			{
				return storiesAvailable;
			}



		}


		public static List<StoryModel> FilterStoriesbySearchKey(List<StoryModel> storiesAvailable, string searchkey)
		{
			try
			{
				if (searchkey != string.Empty)
				{
					//string key = searchkey.Substring(0, 3);
					storiesAvailable = storiesAvailable.FindAll(x => x.Title.ToLower().Contains(searchkey.ToLower())).ToList();

				}

				return storiesAvailable;
			}

			catch (Exception ex)
			{
				return storiesAvailable;
			}

		}

	    public static ContributeStoryModel GetContributeStoryData(int storyID)
	    {
	        ContributeStoryModel obj = new ContributeStoryModel();
	        var data = new GetStories().GetStoryByID(Convert.ToInt32(storyID));
	        obj.StoryID = data.StoryID;
	        obj.Content = data.Content;
	        obj.ContributorID = "";
	        obj.Title = data.Title;
	        return obj;
	    }
    }
}