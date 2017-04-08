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

            model.Stories = storiesAvailable;
            return View(model);
        }



        
        public ActionResult FilterStories(string SelectedGenre, string SelectedType)
        {
            BrowseStoryModel model = new BrowseStoryModel();
            storiesAvailable = new List<StoryModel>();
            storiesAvailable = new GetStories().GetAllStories();
            model.Stories = HomeControllerUtilities.FilterStories(storiesAvailable, SelectedGenre, SelectedType); ;
            model.GenreValues = HomeControllerUtilities.GetGenres();
            model.TypeValues = HomeControllerUtilities.GetTypes();

            return View("BrowseStories", model);

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
                    Session["username"] = credentials.Email;
                    Session["password"] = credentials.Password;
                    AccountController.ShowLogOff = true;
                }
                return Json(result.Message);
            }
            return Json("Error !! Data is null.");
        }
    }
}
