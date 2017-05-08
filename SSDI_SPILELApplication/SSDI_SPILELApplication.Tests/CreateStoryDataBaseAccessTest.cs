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
using System.Threading;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class CreateStoryDataBaseAccessTest
    {
        private string dataBaseName = "spielDBTest";
        [Test]
        public void TestEditorCreateStory()
        {
            ResultCode result;
            ConnStoryTable obj = new ConnStoryTable();
            Thread.Sleep(1000);
            var username = "mdeshpa3@gmail.com";
            obj.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test";
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new DatabaseAccess().CreateStory(obj, username);
            Assert.IsTrue(result.Result); // value saved
        }

        [Test]
        public void TestEditorForExistingStory()
        {
            ResultCode result;
            var username = "mdeshpa3@gmail.com";
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = "Test"; // Already existing story
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new DatabaseAccess().CreateStory(obj, username);
            Assert.IsFalse(result.Result); // value not saved
        }

        [Test]
        public void TestCreateStoryForDatabaseIncrement()
        {
            var username = "mdeshpa3@gmail.com";
            var condition = Builders<ConnStoryTable>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<ConnStoryTable>("StoryTable");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            ResultCode result;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test";
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new DatabaseAccess().CreateStory(obj,username);
            Assert.IsTrue(result.Result); // value saved

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count + 1, countNew); // Value added to database
        }

        [Test]
        public void TestCreateExistingStoryForDatabaseNonIncrement()
        {
            var username = "mdeshpa3@gmail.com";
            var condition = Builders<ConnStoryTable>.Filter.Empty;
            MongoClient mongoClient = new MongoClient();
            var dataBase = mongoClient.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<ConnStoryTable>("StoryTable");
            int count = collection.Find(condition).ToList().Count; // Gives the initial count

            ResultCode result;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title =  "Test"; // Already existing story
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = new DatabaseAccess().CreateStory(obj, username);
            Assert.IsFalse(result.Result); // value not saved

            int countNew = collection.Find(condition).ToList().Count; // new count
            Assert.AreEqual(count, countNew); // Value not added to database
        }
    }
}
