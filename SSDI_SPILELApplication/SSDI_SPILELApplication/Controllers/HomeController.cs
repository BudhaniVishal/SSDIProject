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
        [HttpPost]
        public JsonResult CreateEditorStory(StoryModel data)
        {
            if (data != null )
            {
				if (Session["username"] != null & Session["password"] != null)
				{

					CreateStory story = new CreateStory();
					ResultCode result = story.CreateEditorStory(data);
					return Json(result);
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
		public JsonResult verifyEmail(VerifyEmailModel v)
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
		public JsonResult updatepassword(UpdatepasswordModel v)
		{
			if (v != null)
			{
				if (!v.Password.Equals(v.ConfirmPassword))
				{
					return Json("Password and Confirm Password doesn't match !!");
				}
				String email = Session["email"].ToString();
				UpdatePasswordLL obj = new UpdatePasswordLL();
				ResultCode result = obj.updatepassword(v, email); 

				return Json(result.Message);




			}
			return Json("Error !! Data is null.");
		}




	}
}
