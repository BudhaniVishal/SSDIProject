using DataBaseAccessLayer.ConnectionClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using System.Threading;

namespace DataBaseAccessLayer
{
    public class DatabaseAccess
    {
        public static bool CreateStory(ConnStoryTable story)
        {


            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("spielDB");
            var collection = MongoDB.GetCollection<BsonDocument>("StoryTable");
            BsonDocument task = null;
            task =  collection.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("StoryID")).Limit(1).FirstOrDefault();
            var result = BsonSerializer.Deserialize<ConnStoryTable>(task.ToBsonDocument());
            var StoryID = result.StoryID + 1;
            story.StoryID = StoryID;
            var documnt = story.ToBsonDocument();
            collection.InsertOne(documnt);
            return true;
        }

        private static async Task<ConnStoryTable> GetResult(IMongoCollection<BsonDocument> collec)
        {
            var bsonObject = await collec.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("StoryID")).Limit(1).FirstOrDefaultAsync();
            var result = BsonSerializer.Deserialize<ConnStoryTable>(bsonObject);
            //bsonObject.

            return result;
        }

    }
}
