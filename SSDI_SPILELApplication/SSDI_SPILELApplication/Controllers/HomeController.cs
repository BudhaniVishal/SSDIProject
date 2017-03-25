using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var result = true;
            CreateStory story = new CreateStory();
            var task = story.CreateEditorStory(data);
            return Json(result);
        }
<<<<<<< HEAD

        [HttpPost]
        public JsonResult UserRegistration(UserRegistrationModel data)
        {
            return null;
=======
        [HttpPost]
        public JsonResult Login(LoginModel credentials)
        {
            var userexists = false;
            VerifyLogin v =new VerifyLogin();
           v.verifyuser(credentials);

            return Json(userexists);
>>>>>>> Coomiting Login Model , View and Controller
        }
    }
}