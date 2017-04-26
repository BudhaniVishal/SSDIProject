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
using NUnit.Framework.Internal;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class BrowseContributorStoryTest
    {
        IMongoDatabase dataBase = TestUtilityClass.CreateDataConnection(new MongoClient());
        private string content = string.Empty;
        [Test]
        public void TestBrowseContributorStory()
        {
            content = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            content = content.Replace(" ", "").Replace("/", "").Replace(":","");
            ContributorStoryModel model = new ContributorStoryModel();
            model.StoryID = 22;
            model.Content = content;
            model.ContributorID = content+"@testmail.com";
            model.Title = "Testing Content For Save Contribution Story";
            DatabaseAccess obj = new DatabaseAccess();
            bool result = obj.SaveContributionForStory(model);
            Assert.IsTrue(result);

            var tableCollection = dataBase.GetCollection<ContributorStoryModel>("ContributorStoryTable");
            var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, 22);
            var fields = Builders<ContributorStoryModel>.Projection.Include(p => p.Content).Include(p => p.StoryID);
            var results = tableCollection.Find(condition).Project<ContributorStoryModel>(fields).ToList().AsQueryable();
            if (results.Any())
            {
                Assert.IsTrue((results.Count(s => s.Content.Equals(content))) == 1);
            }

            List<ContributorStoryModel> stories = obj.BrowseContributorStories(content + "@testmail.com");
            Assert.IsTrue(stories.Count == 1);
            Assert.IsTrue(stories[0].Content.Equals(content));
        }
    }
}
