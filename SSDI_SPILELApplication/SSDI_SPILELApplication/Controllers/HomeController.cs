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

namespace SSDI_SPILELApplication.Controllers
{
    public class HomeController : Controller
    {
        static List<StoryModel> storiesAvailable;
        public ActionResult Index()
        {
            if(Session["username"] != null)
            {
                Session.Clear();
            }
            AccountController.ShowLogOff = false;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
			
            return View();
        }
		
		public ActionResult editor()
        {
            ViewBag.Message = "Your application description page.";
            if (Session["username"] != null)
            {
                return View();
            }
            else return View("Index");
        }

        
        public ActionResult BrowseStories()
        {
            BrowseStoryModel model = new BrowseStoryModel();

            storiesAvailable = new List<StoryModel>();
            storiesAvailable = new GetStories().GetAllStories();

            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();
            if (Session["username"] != null && Session["StoryID"] != null && Session["StoryIDViewBag"] != null) // Logic is given for contribute user story when a site visitor is surfing the website!!
            {
                ContributeStoryModel storyDetails = HomeControllerUtilities.GetContributeStoryData(Convert.ToInt32(Session["StoryID"]));
                Session["StoryIDViewBag"] = null;
                return View("ContributeToStory", storyDetails);
            }
            model.Stories = storiesAvailable;
            return View(model);
        }

        public ActionResult BrowseCreatorStories()
        {
            BrowseStoryModel model = new BrowseStoryModel();

            
            storiesAvailable = new List<StoryModel>();
            var username = Session["username"].ToString();
            GetStories storyobj = new GetStories();
            var results = storyobj.getCreatedStories(username);
            for (int i = 0; i < results.Count; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = results[i].Title;
                story.Content = results[i].Content;
				story.StoryID = results[i].StoryID;
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
            var username = Session["username"].ToString();
            GetStories storyobj = new GetStories();

            var results = storyobj.getContributorStories(username);
            for (int i = 0; i < results.Count; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = results[i].Title;
                story.Content = results[i].Content;
				story.StoryID = results[i].StoryID;
                storiesAvailable.Add(story);
            }

            ViewBag.GenreValue = "Select";
            ViewBag.TypeValue = "Type";


            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            model.Stories = storiesAvailable;
            return View(model);
        }

        public ActionResult ApproveContributedStories(int storyID)
        {
            BrowseStoryModel model = new BrowseStoryModel();
            storiesAvailable = new List<StoryModel>();
            
            storiesAvailable = new GetStories().BrowseContributedStoriesForEditor(storyID);
            model.Stories = storiesAvailable;

            return View("ApproveContributedStories", model);
        }

        public ActionResult DeleteRestStories(int storyID, string ContributorID)
        {
            BrowseStoryModel model = new BrowseStoryModel();
            bool storiesDeleted = false;

            storiesDeleted = new DeleteStories().DeleteRestContributedStories(storyID, ContributorID);
            storiesAvailable = new List<StoryModel>();
            storiesAvailable = new GetStories().GetAllStories();

            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            model.Stories = storiesAvailable;
            

            return View("BrowseStories", model);
        }


        public ActionResult BrowseSuggestions(int storyID) {
			StorySuggestionsModel model = new StorySuggestionsModel();
			model.CurrentStory = new GetStories().GetStoryByID(storyID);
			List<SuggestionModel> suggestionsAvailable = new List<SuggestionModel>();
			if (Session["username"] == null) {
				return RedirectToAction("Index");
			}
			var username = Session["username"].ToString();
			
			GetSuggestions suggestionobj = new GetSuggestions();

			var results = suggestionobj.ReturnSuggestions(storyID);
			for (int i = 0; i < results.Count; i++) {
				SuggestionModel s = new SuggestionModel();
				s.DatePosted = results[i].DatePosted;
				s.Content = results[i].Content;
				s.OwningStoryID = results[i].OwningStoryID;
				s.SuggestionID = results[i].SuggestionID;
				suggestionsAvailable.Add(s);
			}

			model.Suggestions = suggestionsAvailable;
			return View(model);
		}

		public ActionResult ViewSuggestion(int suggestionID) {
			ViewSuggestionModel model = new ViewSuggestionModel();
			SuggestionTable table = new GetSuggestions().ReturnSuggestionByID(suggestionID);
			SuggestionModel sug = new SuggestionModel();

			if (Session["username"] == null) {
				return RedirectToAction("Index");
			}

			sug.Content = table.Content;
			sug.DatePosted = table.DatePosted;
			sug.OwningStoryID = table.OwningStoryID;
			sug.SuggestionID = table.SuggestionID;

			model.Suggestion = sug;
			model.CurrentStory = new GetStories().GetStoryByID(sug.OwningStoryID);

			return View(model);
		}


		
		public ActionResult FilterStories(string selectedGenre, string selectedType,string filter , string filterbykey, BrowseStoryModel key, int? storyID)
        {
	        BrowseStoryModel model = new BrowseStoryModel();
			
            if (storyID == null)
            {
                if (!string.IsNullOrEmpty(filter))
                {

                    storiesAvailable = new List<StoryModel>();
                    storiesAvailable = new GetStories().GetAllStories();
                    model.Stories = HomeControllerUtilities.FilterStories(storiesAvailable, selectedGenre, selectedType);

                    model.GenreValues = HomeControllerUtilities.GetGenres();
                    model.TypeValues = HomeControllerUtilities.GetTypes();
                }
                if (!string.IsNullOrEmpty(filterbykey))
                {


                    storiesAvailable = new List<StoryModel>();

                    storiesAvailable = new GetStories().GetAllStories();
                    model.Stories = HomeControllerUtilities.FilterStoriesbySearchKey(storiesAvailable, key.SearchKey);
                    model.GenreValues = HomeControllerUtilities.GetGenres();
                    model.TypeValues = HomeControllerUtilities.GetTypes();
                }
                return View("BrowseStories", model);
            }
            if (Session["username"] == null)
            {
                Session["StoryID"] = storyID;
                Session["StoryIDViewBag"] = storyID;
                return RedirectToAction("Login", "Account");
            }
            Session["StoryID"] = storyID;  //GetStoryBased on the story ID
            ContributeStoryModel storyDetails = HomeControllerUtilities.GetContributeStoryData(Convert.ToInt32(storyID));
            return View("ContributeToStory", storyDetails);

        }

		


		public ActionResult FilterCreatorStories(string selectedGenre, string selectedType)
        {
            BrowseStoryModel model = new BrowseStoryModel();
            storiesAvailable = new List<StoryModel>();
            var username = Session["username"].ToString();
            storiesAvailable = new GetStories().getCreatedStories(username);
            model.Stories = HomeControllerUtilities.FilterStories(storiesAvailable, selectedGenre, selectedType); ;
            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            return View("BrowseStories", model);

        }
        public ActionResult FilterContributorStories(string selectedGenre, string selectedType)
        {
            BrowseStoryModel model = new BrowseStoryModel();
            storiesAvailable = new List<StoryModel>();
            var username = Session["username"].ToString();
            storiesAvailable = new GetStories().getContributorStories(username);
            model.Stories = HomeControllerUtilities.FilterStories(storiesAvailable, selectedGenre, selectedType); ;
            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            return View("BrowseStories", model);

        }

        [HttpPost]
        public JsonResult CreateEditorStory(StoryModel data)
        {
            if (data != null )
            {
				if (Session["username"] != null & Session["password"] != null)
				{

                    var username = Session["username"].ToString();
                    CreateStory story = new CreateStory();
					ResultCode result = story.CreateEditorStory(data,username);
					return Json(result.Message);
				}
				else
				{
					return Json("Invalid Session . Please Login");

				}
            }
            return Json("Error !! Data is null.");
        }

        [HttpPost]
        public JsonResult UserRegistration(UserRegistrationModel data)
        {
            if(data != null)
            {
                if (!data.Password.Equals(data.ConfirmPassword))
                {
                    return Json("Password and Confirm Password doesn't match !!");
                }
                CreateUserRegistration obj = new CreateUserRegistration();
                ResultCode result = obj.RegisterUser(data);
                return Json(result.Message);
            }
            return Json("Error !! Data is null.");

        }
        
        [HttpPost]
        public JsonResult Login(LoginModel credentials)
        {
            if (credentials != null)
            {
                VerifyLogin obj = new VerifyLogin();
                ResultCode result = obj.LoginUser(credentials);
                if (result.Result)
                {
                    Session["username"] = credentials.Email;
                    Session["password"] = credentials.Password;
                    AccountController.ShowLogOff = true;
                }
                return Json(result.Message);
            }
            return Json("Error !! Data is null.");
        }
		[HttpPost]
		public JsonResult VerifyEmail(VerifyEmailModel v)
		{
			if (v != null)
			{
				Session["email"] = v.Email;
				VerifyEmailLL obj = new VerifyEmailLL();
				ResultCode result = obj.VerifyEmail(v);
				
					return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}


		[HttpPost]
		public JsonResult UpdatePassword(UpdatepasswordModel v)
		{
			if (v != null)
			{
				if (!v.Password.Equals(v.ConfirmPassword))
				{
					return Json("Password and Confirm Password doesn't match !!");
				}
				String email = Session["email"].ToString();
				UpdatePasswordLL obj = new UpdatePasswordLL();
				ResultCode result = obj.UpdatePassword(v, email); 

				return Json(result.Message);
			}
			return Json("Error !! Data is null.");
		}

        public ActionResult ContributeToStory(int? storyID, ContributeStoryModel obj)
        {
            return View();
        }
        public ActionResult ContributeStorySave(string[] textAnswer)
        {
            ContributeStoryModel obj = new ContributeStoryModel();
            obj.StoryID = Convert.ToInt32(Session["StoryID"]);
            obj.ContributorID = Session["username"].ToString();
            obj.ContributionText = textAnswer[0];
            //Save the data
            var result = new CreateStory().SaveContributionForStory(obj);
            //Retrieve the data
            obj = HomeControllerUtilities.GetContributeStoryData(Convert.ToInt32(Session["StoryID"]));
            return View("ContributeToStory", obj);
        }

        public ActionResult StoryDetails(int? storyID)
        {
            if (storyID != null)
            {
                StoryModel obj = new GetStories().GetStoryByID(Convert.ToInt32(storyID));
                return View(obj);
            }
            return View();
        }

    }
}
