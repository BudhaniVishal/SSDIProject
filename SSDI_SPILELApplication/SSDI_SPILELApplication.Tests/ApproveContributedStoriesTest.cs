using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer;
using DataBaseAccessLayer.ConnectionClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class ApproveContributedStoriesTest
    {
        IMongoDatabase dataBase = TestUtilityClass.CreateDataConnection(new MongoClient());
        [Test]
        public void TestApprovedContributedStory()
        {
            string emailAddress = "test@test.com";
            int storyID = 1;
            string content = "TestContent";

            ContributorStoryModel model = new ContributorStoryModel();
            model.StoryID = storyID;
            model.Content = content;
            model.ContributorID = emailAddress;
            model.Title = "Story1";
            DatabaseAccess obj = new DatabaseAccess();
            bool result = obj.SaveContributionForStory(model);
            Assert.IsTrue(result);

            var tableCollection = dataBase.GetCollection<ContributorStoryModel>("ContributorStoryTable");
            var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, storyID);
            var fields = Builders<ContributorStoryModel>.Projection.Include(p => p.Content).Include(p => p.StoryID);
            var results = tableCollection.Find(condition).Project<ContributorStoryModel>(fields).ToList().AsQueryable();
            if (results.Any())
            {
                Assert.IsTrue((results.Count(s => s.Content.Equals(content))) == 1);
            }

            List<ContributorStoryModel> stories = obj.BrowseContributorStories(content + "@testmail.com");
            Assert.IsTrue(stories.Count == 1);
            Assert.IsTrue(stories[0].Content.Equals(content));

            Assert.IsTrue(obj.DeleteRestContributedStories(storyID, emailAddress));

            var collection = dataBase.GetCollection<ContributorStoryModel>("ContributorStoryTable");
            var cond = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, storyID);
            var field = Builders<ContributorStoryModel>.Projection.Include(p => p.Content).Include(p => p.StoryID);
            var queryResult = tableCollection.Find(condition).Project<ContributorStoryModel>(fields).ToList().AsQueryable();
            Assert.True(!queryResult.Any());

            var approvedResult = obj.GetStoryByID(storyID);
            Assert.IsTrue(approvedResult.Content.Contains(content));
        }
    }
}
