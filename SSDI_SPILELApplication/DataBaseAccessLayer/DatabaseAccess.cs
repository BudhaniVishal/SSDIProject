using DataBaseAccessLayer.ConnectionClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DataBaseAccessLayer
{
    public class DatabaseAccess
    {
        public static bool CreateStory(ConnStoryTable story)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("spielDB");
            var Collec = MongoDB.GetCollection<BsonDocument>("StoryTable");
            var documnt = new BsonDocument
            {
                {"Title","Dell"},
                {"Scenario","laptop"},
                {"Content",""},
                {"StoryID",2},
                {"Type","NonFiction"},
                {"Genre","Thriller"},
                {"To","03/05/2017"},
                {"From","07/05/2017"}

            };
            Collec.InsertOneAsync(documnt);
            //Console.ReadLine();
            return true;

        }
    }
}
