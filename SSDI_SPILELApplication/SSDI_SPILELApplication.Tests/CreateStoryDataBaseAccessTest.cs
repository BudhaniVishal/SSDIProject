using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer;
using NUnit.Framework;
using DataBaseAccessLayer.ConnectionClass;
using MongoDB.Driver;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class CreateStoryDataBaseAccessTest
    {
        private string dataBaseName = "spielDBTest";
        [Test]
        public void TestEditorCreateStory()
        {
            bool result = false;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test";
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new MockDataBaseAccess().CreateStory(obj);
            Assert.IsTrue(result); // value saved
        }

        [Test]
        public void TestEditorForExistingStory()
        {
            bool result = true;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = "Test"; // Already existing story
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new MockDataBaseAccess().CreateStory(obj);
            Assert.IsFalse(result); // value not saved
        }

        [Test]
        public void TestCreateStoryForDatabaseIncrement()
        {
            var condition = Builders<ConnStoryTable>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<ConnStoryTable>("StoryTable");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            bool result = false;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test";
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new MockDataBaseAccess().CreateStory(obj);
            Assert.IsTrue(result); // value saved

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count + 1, countNew); // Value added to database
        }

        [Test]
        public void TestCreateExistingStoryForDatabaseNonIncrement()
        {
            var condition = Builders<ConnStoryTable>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<ConnStoryTable>("StoryTable");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            bool result = true;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title =  "Test"; // Already existing story
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new MockDataBaseAccess().CreateStory(obj);
            Assert.IsFalse(result); // value not saved

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count, countNew); // Value not added to database
        }
    }
}
