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
        public ActionResult Index()
        {
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
        [HttpPost]
        public JsonResult CreateEditorStory(StoryModel data)
        {
            CreateStory story = new CreateStory();
            ResultCode result = story.CreateEditorStory(data);
            return Json(result.Message);
        }

        [HttpPost]
        public JsonResult UserRegistration(UserRegistrationModel data)
        {
            if (!data.Password.Equals(data.ConfirmPassword))
            {
                return Json("Password and Confirm Password doesn't match !!");
            }
            CreateUserRegistration obj = new CreateUserRegistration();
            ResultCode result = obj.RegisterUser(data);
            return Json(result.Message);
        }
        
        [HttpPost]
        public JsonResult Login(LoginModel credentials)
        {
            VerifyLogin obj =new VerifyLogin();
            ResultCode result = obj.LoginUser(credentials);
            return Json(result.Message);
        }
    }
}
