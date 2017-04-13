using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.LogicLayer;
using UserRegistrationModel = SSDI_SPILELApplication.Models.UserRegistrationModel;
using SSDI_SPILELApplication.Utilities;

namespace SSDI_SPILELApplication.Tests.MockClasses
{
	public class MockHomeController : Controller
	{
        static List<StoryModel> storiesAvailable;

        [HttpPost]
		public JsonResult VerifyEmail(VerifyEmailModel v)
		{
			if (v != null)
			{
				
				MockVerifyEmailLL obj = new MockVerifyEmailLL();
				ResultCode result = obj.VerifyEmail(v);

				return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}
		[HttpPost]
		public JsonResult updatepassword(UpdatepasswordModel v,string e )
		{
			if (v != null)
			{
				if (!v.Password.Equals(v.ConfirmPassword))
				{
					return Json("Password and Confirm Password doesn't match !!");
				}
				String email = e;
				UpdatePasswordLL obj = new UpdatePasswordLL();
				ResultCode result = obj.updatepassword(v, email);

				return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}


        public ActionResult BrowseCreatorStories()
        {
            BrowseStoryModel model = new BrowseStoryModel();


            storiesAvailable = new List<StoryModel>();
            var username = "mdeshpa3@gmail.com";
            GetStories storyobj = new GetStories();
            var results = storyobj.getCreatedStories(username);
            for (int i = 0; i < results.Count; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = results[i].Title;
                story.Content = results[i].Content;
                storiesAvailable.Add(story);
            }

            ViewBag.GenreValue = "Select";
            ViewBag.TypeValue = "Type";


            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            model.Stories = storiesAvailable;
            return View(model);
        }

        public ActionResult BrowseContributorStories()
        {
            BrowseStoryModel model = new BrowseStoryModel();


            storiesAvailable = new List<StoryModel>();
            var username = "mdeshpa3@gmail.com";
            GetStories storyobj = new GetStories();

            var results = storyobj.getContributorStories(username);
            for (int i = 0; i < results.Count; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = results[i].Title;
                story.Content = results[i].Content;
                storiesAvailable.Add(story);
            }

            ViewBag.GenreValue = "Select";
            ViewBag.TypeValue = "Type";


            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            model.Stories = storiesAvailable;
            return View(model);
        }

        public JsonResult CreateEditorStory(StoryModel data)
        {
            if (data != null)
            {


                var username = "mdeshpa3@gmail.com";
                    CreateStory story = new CreateStory();
                    ResultCode result = story.CreateEditorStory(data, username);
                    return Json(result.Message);
                
            }
            return Json("Error !! Data is null.");
        }




    }
}
