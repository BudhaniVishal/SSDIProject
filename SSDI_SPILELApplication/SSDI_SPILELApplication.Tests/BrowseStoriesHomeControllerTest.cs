using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataBaseAccessLayer;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Utilities;

namespace SSDI_SPILELApplication.Tests
{
	[TestFixture]
	public class BrowseStoriesHomeControllerTest
	{
		[Test]
		public void TestBrowseStoriesHomeController()
		{
			HomeController obj = new HomeController();
			var result = obj.BrowseStories();
			Assert.IsNotNull(result);

			BrowseStoryModel model = ((ViewResultBase) result).Model as BrowseStoryModel;
			ValidateListItems(model, HomeControllerUtilities.GetGenres(), HomeControllerUtilities.GetTypes());

			Assert.IsNotNull(model);
			Assert.IsTrue(model.Stories.Count >= 0);
		}


		private void ValidateListItems(BrowseStoryModel model, List<SelectListItem> getGenres,
			List<SelectListItem> getTypes)
		{
			int genreCount = getGenres.Count;
			int typeCount = getTypes.Count;
			int count = (genreCount > typeCount) ? genreCount : typeCount;

			for (int i = 0; i < count; i++)
			{
				if (i < genreCount)
				{
					Assert.AreEqual(model.GenreValues[i].Text, getGenres[i].Text);
				}
				if (i < typeCount)
				{
					Assert.AreEqual(model.TypeValues[i].Text, getTypes[i].Text);
				}
			}
		}

		[Test]
		public void TestFilterStories()
		{
			var genre = HomeControllerUtilities.GetGenres();
			var type = HomeControllerUtilities.GetTypes();
			int genreCount = genre.Count;
			int typeCount = type.Count;
			int count = (genreCount > typeCount) ? genreCount : typeCount;

			for (int i = 0; i < genreCount; i++)
			{
				var genreSearchText = genre[0].Text;
				for (int j = 0; j < typeCount; j++)
				{
					var typeSearchText = type[0].Text;
					ValidateListItems(genreSearchText, typeSearchText);
				}
			}

		}


		private void ValidateListItems(string genreSearchText, string typeSearchText)
		{
			HomeController obj = new HomeController();
			BrowseStoryModel m = new BrowseStoryModel();
			var result = obj.FilterStories(genreSearchText, typeSearchText, "filter", string.Empty, m,0);

			BrowseStoryModel model = ((ViewResultBase) result).Model as BrowseStoryModel;
			Assert.IsNotNull(model);
			if (model.Stories.Count > 0)
			{
				foreach (var item in model.Stories)
				{
					Assert.AreEqual(item.Genre, genreSearchText);
					Assert.AreEqual(item.Type, typeSearchText);
				}
			}
		}

		[Test]
		public void TestFilterStoriesForEmptySearchStrings()
		{
			HomeController obj = new HomeController();
			BrowseStoryModel m = new BrowseStoryModel();
			m.SearchKey = string.Empty;
			var result = obj.FilterStories(string.Empty, string.Empty, "filter", string.Empty, m,0);
			BrowseStoryModel model = ((ViewResultBase) result).Model as BrowseStoryModel;
			Assert.IsNotNull(model);

			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			var list = objDatabaseAccess.GetAllStories();

			Assert.AreEqual(model.Stories.Count, list.Count);
			for (int i = 0; i < model.Stories.Count; i++)
			{
				Assert.AreEqual(model.Stories[i].Title, list[i].Title);
			}


		}
	}
}