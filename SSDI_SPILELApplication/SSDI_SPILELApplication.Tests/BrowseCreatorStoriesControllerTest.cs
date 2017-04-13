using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.Tests
{
    class BrowseCreatorStoriesControllerTest
    {
        private readonly string errorMessage = "Error !! Data is null.";
        [Test]
        public void BrowseCreatorStoryWhenDataNull()
        {
            //HomeController obj = new HomeController();
            //ActionResult result = obj.BrowseCreatorStories();
            //Assert.AreEqual(result.Data.ToString(), errorMessage);
        }

    }
}
