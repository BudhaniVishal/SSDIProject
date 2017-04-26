using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Driver;
using NUnit.Framework;
using SSDI_SPILELApplication.Tests.MockClasses;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class SaveContributorForStoryTest
    {
        IMongoDatabase dataBase = TestUtilityClass.CreateDataConnection(new MongoClient());

        [Test]
        public void TestSaveContributionForStory()
        {
            int randomID = GetRandomID();
            string content = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            ContributorStoryModel model = new ContributorStoryModel();
            model.StoryID = randomID;
            model.Content = content;
            model.ContributorID = "test@testmail.com";
            model.Title = "Testing Content For Save Contribution Story";
            DatabaseAccess obj = new DatabaseAccess();
            bool result = obj.SaveContributionForStory(model);
            Assert.IsTrue(result);

            var tableCollection = dataBase.GetCollection<ContributorStoryModel>("ContributorStoryTable");
            var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, randomID);
            var fields = Builders<ContributorStoryModel>.Projection.Include(p => p.Content).Include(p => p.StoryID);
            var results = tableCollection.Find(condition).Project<ContributorStoryModel>(fields).ToList().AsQueryable();
            if (results.Any())
            {
                Assert.IsTrue((results.Count(s => s.Content.Equals(content)))==1);
            }
        }

        private int GetRandomID()
        {
            Random random = new Random();
            return random.Next(0, 1000000);
        }

        [Test]
        public void TestSaveContributionHomeController()
        {
            int randomID = GetRandomID();
            string content = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            string[] array = { content };

            Assert.IsTrue(MockHomeController.ContributeStorySave(array, randomID));

            var tableCollection = dataBase.GetCollection<ContributorStoryModel>("ContributorStoryTable");
            var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, randomID);
            var fields = Builders<ContributorStoryModel>.Projection.Include(p => p.Content).Include(p => p.StoryID);
            var results = tableCollection.Find(condition).Project<ContributorStoryModel>(fields).ToList().AsQueryable();
            if (results.Any())
            {
                Assert.IsTrue((results.Count(s => s.Content.Equals(content))) == 1);
            }
        }
    }
}
