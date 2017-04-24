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
	public class TestSearchStoriesByKey
	{
		[Test]
		public void TestFilterStoriesByValidSearchKey()
		{
			HomeController obj = new HomeController();
			BrowseStoryModel m = new BrowseStoryModel();
			m.SearchKey = "Del";
			var result = obj.FilterStories(string.Empty, string.Empty, string.Empty,"filterbyKey",m,0);
			BrowseStoryModel model = ((ViewResultBase) result).Model as BrowseStoryModel;
			Assert.IsNotNull(model);
			int expected = model.Stories.Count;
			int actual = 0;
			
			for (int i = 0; i < model.Stories.Count; i++)
			{
				if (model.Stories[i].Title.Contains(m.SearchKey)) ;
				{
					actual++;
				}
			}
			Assert.AreEqual(actual,expected);
		}

		[Test]
		public void TestFilterStoriesByEmptyString()
		{
			HomeController obj = new HomeController();
			BrowseStoryModel m = new BrowseStoryModel();
			m.SearchKey = String.Empty;
			var result = obj.FilterStories(string.Empty, string.Empty, string.Empty, "filterbyKey", m,0);
			BrowseStoryModel model = ((ViewResultBase)result).Model as BrowseStoryModel;
		
			DatabaseAccess objDatabaseAccess = new DatabaseAccess();
			var list = objDatabaseAccess.GetAllStories();

			Assert.AreEqual(model.Stories.Count, list.Count);
			
		}
		[Test]
		public void TestFilterStoriesBySpaces()
		{
			HomeController obj = new HomeController();
			BrowseStoryModel m = new BrowseStoryModel();
			m.SearchKey = "   ";
			var result = obj.FilterStories(string.Empty, string.Empty, string.Empty, "filterbyKey", m,0);
			BrowseStoryModel model = ((ViewResultBase)result).Model as BrowseStoryModel;
			Assert.AreEqual(model.Stories.Count, 0);

		}
	}
}

