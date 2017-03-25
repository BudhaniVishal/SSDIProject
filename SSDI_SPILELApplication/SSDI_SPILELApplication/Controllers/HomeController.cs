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
        public JsonResult AjaxMethod(StoryModel data)
        {
            var result = true;
            CreateStory story = new CreateStory();
            var task = story.CreateEditorStory(data);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UserRegistration(UserRegistrationModel data)
        {
            return null;
        }
    }
}