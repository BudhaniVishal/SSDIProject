using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.LogicLayer;
using UserRegistrationModel = SSDI_SPILELApplication.Models.UserRegistrationModel;

namespace SSDI_SPILELApplication.Controllers
{
    public class HomeController : Controller
    {
        static List<StoryModel> storiesAvailable;
        public ActionResult Index()
        {
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

            return View();
        }

        
        public ActionResult BrowseStories()
        {
            BrowseStoryModel model = new BrowseStoryModel();
            
            storiesAvailable = new List<StoryModel>();
            
            //string strDDLValue = Request.Form["genre"].ToString();
            for (int i = 1; i<= 10; i++){
                StoryModel story = new StoryModel();
                story.Title = "test Story" + i;
                story.Content = "test Story" + i;
                //story.Genres = genre;
                //story.Types = type;
                storiesAvailable.Add(story);
            }
            
            //ViewBag.Test = genre;
            ViewBag.GenreValue = "Select";
            ViewBag.TypeValue = "Type";


            model.GenreValues = GetGenres();
            model.TypeValues = GetTypes();

            model.Stories = storiesAvailable;
            return View(model);
            //return View(storiesAvailable);
        }

        public ActionResult TestView()
        {
            BrowseStoryModel model = new BrowseStoryModel();

            storiesAvailable = new List<StoryModel>();

            //string strDDLValue = Request.Form["genre"].ToString();
            for (int i = 1; i <= 10; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = "test Story" + i;
                story.Content = "test Story" + i;
                //story.Genres = genre;
                //story.Types = type;
                storiesAvailable.Add(story);
            }

            //ViewBag.Test = genre;
            ViewBag.GenreValue = "Select";
            ViewBag.TypeValue = "Type";


            model.GenreValues = GetGenres();
            model.TypeValues = GetTypes();

            model.Stories = storiesAvailable;
            return View(model);
            //return View(storiesAvailable);
        }

        private List<SelectListItem> GetTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Type1",
                Value = "Type1"
            });
            items.Add(new SelectListItem
            {
                Text = "Type2",
                Value = "Type2",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Type3",
                Value = "Type3"
            });
            return items;
        }

        private List<SelectListItem> GetGenres()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Swimming",
                Value = "Swimming"
            });
            items.Add(new SelectListItem
            {
                Text = "Cycling",
                Value = "Cycling",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Running",
                Value = "Running"
            });
            return items;
        }

        
        public ActionResult FilterStories(string SelectedGenre, string SelectedType)
        {
            BrowseStoryModel model = new BrowseStoryModel();

            var t1 = Request.Form["GenreVal"];
            var t = ViewBag.GenreValue;
            var k = ViewBag.TypeValue;
            storiesAvailable = new List<StoryModel>();

            //string strDDLValue = Request.Form["genre"].ToString();

            for (int i = 1; i <= 10; i++)
            {
                StoryModel story = new StoryModel();
                story.Title = "A Story" + i;
                story.Content = "A Story" + i;
                storiesAvailable.Add(story);
            }
            model.Stories = storiesAvailable;
            model.GenreValues = GetGenres();
            model.SelectedGenre = GetGenres().Any(x => x.Selected).ToString();
            model.TypeValues = GetTypes();


            ViewBag.GenreValue = "Family";
            ViewBag.TypeValue = "Type";
            //return View("DisplayAvailableStories", storiesAvailable);
            return View("DisplayAvailableStories", model);
            //http://stackoverflow.com/questions/21170064/how-to-refresh-only-part-of-the-index-page-in-mvc-5
            //http://stackoverflow.com/questions/23851323/how-to-access-a-view-element-from-controller
            //http://www.binaryintellect.net/articles/4a00a9ce-73e5-4d89-aaae-2d835eca0854.aspx

        }

        protected void ddlselect_Changed(object sender, EventArgs e)
        {

        }

        [HttpPost]
        public JsonResult CreateEditorStory(StoryModel data)
        {
            if (data != null)
            {
                CreateStory story = new CreateStory();
                ResultCode result = story.CreateEditorStory(data);
                return Json(result);
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
                    AccountController.ShowLogOff = true;
                }
                return Json(result.Message);
            }
            return Json("Error !! Data is null.");
        }
    }
}
