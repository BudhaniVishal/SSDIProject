using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Tests.MockClasses;

namespace SSDI_SPILELApplication.Tests
{
    public class CreateEditorStoryHomeControllerTest
    {
        private readonly string errorMessage = "Error !! Data is null.";
        [Test]
        public void CreateEditorStoryForNullValues()
        {
            HomeController obj = new HomeController();
            JsonResult result = obj.CreateEditorStory(null);
            Assert.AreEqual(result.Data.ToString(), errorMessage);
        }

        [Test]
        public void CreateEditorStoryCorrectValues()
        {
            MockHomeController obj = new MockHomeController();
            StoryModel model = new StoryModel();
            model.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test Story";
            model.Content = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test Story Content";
            model.Type = "Short Stories";
            model.Genre = "Action/Adventure";
            model.From = DateTime.Today;
            model.To = DateTime.Now.AddDays(1);
            model.Scenario = "Test Scenario";
            JsonResult result = obj.CreateEditorStory(model);
            Assert.AreNotEqual(result.Data.ToString(), errorMessage);
        }
    }
}
