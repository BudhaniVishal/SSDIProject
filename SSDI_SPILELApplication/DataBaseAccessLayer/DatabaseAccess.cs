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
            task = collection.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("StoryID")).Limit(1).FirstOrDefault();
            var result = BsonSerializer.Deserialize<ConnStoryTable>(task.ToBsonDocument());
            var StoryID = result.StoryID + 1;
            story.StoryID = StoryID;
            var documnt = story.ToBsonDocument();
            collection.InsertOne(documnt);
            return true;
        }
        public static string LoginUser(LoginCheckDLLModel user)
        {
            var mongoClient = new MongoClient();
            var mongoDB = mongoClient.GetDatabase("spielDB");


            IMongoCollection<UserRegistrationModel> collection = mongoDB.GetCollection<UserRegistrationModel>("UserRegistration");
            var condition = Builders<UserRegistrationModel>.Filter.Eq(p => p.EmailAddress, user.Email);
            
            var fields = Builders<UserRegistrationModel>.Projection.Include(p => p.EmailAddress).Include(p => p.Password).Include(p=>p.UserType).Include(p=>p.IsUserVerified);
            var results = collection.Find(condition).Project<UserRegistrationModel>(fields).ToList().AsQueryable();
            var res = false;
            if (results.Count() == 1)
            {
                var data = results.FirstOrDefault();
                if (data != null)
                {
                    if (data.Password.Equals(user.Password))
                    {
                        if (data.UserType.Equals("WRITER"))
                        {
                            return "Writer Login Successful !!";
                        }
                        if (data.UserType.Equals("EDITOR") && data.IsUserVerified.AsBoolean)
                        {
                            return "Editor Login Successful !!";
                        }
                        return "Editor not verified yet ";
                    }
                    return "Incorrect Password !!";
                }

                return "Network Issue, Please try again !!";
            }

            return "Invalid Email Address";

        }

        public static bool RegisterUser(UserRegistrationModel modelData)
        {
            var client = new MongoClient();
            var mongoDb = client.GetDatabase("spielDB");


            IMongoCollection<UserRegistrationModel> collection = mongoDb.GetCollection<UserRegistrationModel>("UserRegistration");
            var condition = Builders<UserRegistrationModel>.Filter.Eq(p => p.EmailAddress, modelData.EmailAddress);
            var fields = Builders<UserRegistrationModel>.Projection.Include(p => p.EmailAddress).Include(p=>p.FirstName);
            var results = collection.Find(condition).Project<UserRegistrationModel>(fields).ToList().AsQueryable();
            if (results.Any())
            {
                // user exists return;
                return false;
            }
            else
            {
                if (modelData.UserType.Equals("WRITER"))
                {
                    modelData.IsUserVerified = true;
                }
                else
                {
                    modelData.IsUserVerified = false;
                }
                //Insert to DB values

                var collectionName = mongoDb.GetCollection<BsonDocument>("UserRegistration");
                var document = modelData.ToBsonDocument();
                collectionName.InsertOne(document);
                return true;
            }
        }
    }
}
