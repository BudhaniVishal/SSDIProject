﻿using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSDI_SPILELApplication.LogicLayer;

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
            if(story.CreateEditorStory(data))
            return Json("Story Created !!");
            return Json("Story with same title exists !!");
        }

        [HttpPost]
        public JsonResult UserRegistration(UserRegistrationModel data)
        {
            if (!data.Password.Equals(data.ConfirmPassword))
            {
                return Json("Password and Confirm Password doesn't match !!");
            }
            CreateUserRegistration obj = new CreateUserRegistration();
            if (obj.RegisterUser(data))
            {
                if (data.UserType == UserType.EDITOR)
                {
                    data.MessageString = "Editor registration is successfull!! You will receive a confirmation email !!";
                    return Json("Editor registration is successfull!! You will receive a confirmation email !!");
                }
                data.MessageString = "Writer registration is successfull";
                return Json("Writer registration is successfull !!");
            }
            data.MessageString = "Email Id Exists, Please try again !!";
            return Json("Email Id Exists, Please try again !!");
        }
        
        [HttpPost]
        public JsonResult Login(LoginModel credentials)
        {
            VerifyLogin obj =new VerifyLogin();
            return Json(obj.LoginUser(credentials));
        }
    }
}
