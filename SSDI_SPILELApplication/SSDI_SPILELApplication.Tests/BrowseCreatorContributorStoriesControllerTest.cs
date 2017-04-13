using DataBaseAccessLayer;
using NUnit.Framework;
using SSDI_SPILELApplication.Controllers;
using SSDI_SPILELApplication.Models;
using SSDI_SPILELApplication.Tests.MockClasses;
using SSDI_SPILELApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.Tests
{
    class BrowseCreatorContributorStoriesControllerTest
    {
        [Test]
        public void TestBrowseCreatorStoriesHomeController()
        {
            MockHomeController obj = new MockHomeController();
            var result = obj.BrowseCreatorStories();
            Assert.IsNotNull(result);

            BrowseStoryModel model = ((ViewResultBase)result).Model as BrowseStoryModel;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Stories.Count >= 0);
        }

        [Test]
        public void TestBrowseContributorStoriesHomeController()
        {
            MockHomeController obj = new MockHomeController();
            var result = obj.BrowseContributorStories();
            Assert.IsNotNull(result);

            BrowseStoryModel model = ((ViewResultBase)result).Model as BrowseStoryModel;

            Assert.IsNotNull(model);
            Assert.IsTrue(model.Stories.Count >= 0);
        }

    }
}
