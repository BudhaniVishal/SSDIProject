using System;
using DataBaseAccessLayer.ConnectionClass;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using DataBaseAccessLayer.Interfaces;
using System.Collections.Generic;

namespace DataBaseAccessLayer
{
    public class MockDataBaseAccess : IDatabaseAccess
    {
        private static string dataBaseName = "spielDBTest";
        public ResultCode CreateStory(ConnStoryTable story,string username)
        {
            ResultCode resultCode = new ResultCode();
            try
            {
                var tableCollection = CreateDataConnection(new MongoClient())
                    .GetCollection<ConnStoryTable>("StoryTable");
                var condition = Builders<ConnStoryTable>.Filter.Eq(p => p.Title, story.Title);
                var fields = Builders<ConnStoryTable>.Projection.Include(p => p.Title).Include(p => p.StoryID);
                var results = tableCollection.Find(condition).Project<ConnStoryTable>(fields).ToList().AsQueryable();
                if (results.Count() == 1)
                {
                    // story exists
                    resultCode.Result = false;
                    resultCode.Message = "Story with same title exists !!";
                    return resultCode;
                }
                var collection = CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("StoryTable");
                BsonDocument task = null;
                task =
                    collection.Find(new BsonDocument())
                        .Sort(Builders<BsonDocument>.Sort.Descending("StoryID"))
                        .Limit(1)
                        .FirstOrDefault();

                ConnStoryTable result = null;
                if (task != null)
                {
                    result = BsonSerializer.Deserialize<ConnStoryTable>(task.ToBsonDocument());
                }
                var StoryID = (task != null) ? (result.StoryID + 1) : 1; // If table is empty, push value as 1.

                story.StoryID = StoryID;
                var documnt = story.ToBsonDocument();
                collection.InsertOne(documnt);

                var Collect = CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("CreatorStoryTable");
                CreatorStoryModel crtrObj = new CreatorStoryModel();
                crtrObj.StoryID = StoryID;
                crtrObj.EditorID = username;
                var documnt2 = crtrObj.ToBsonDocument();
                Collect.InsertOne(documnt2);
                resultCode.Result = true;
                resultCode.Message = "Story Created !!";
                return resultCode;
            }
            catch (Exception ex)
            {
                resultCode.Result = false;
                resultCode.Message = "Error occured, Please try again !!";
                return resultCode;
            }

        }

        private IMongoDatabase CreateDataConnection(MongoClient obj)
        {
            return obj.GetDatabase(dataBaseName);
        }

        public ResultCode LoginUser(UserRegistrationModel user)
        {
            ResultCode resultCode = new ResultCode();
            try
            {
                IMongoCollection<UserRegistrationModel> collection = CreateDataConnection(new MongoClient()).GetCollection<UserRegistrationModel>("UserRegistration");
                var condition = Builders<UserRegistrationModel>.Filter.Eq(p => p.EmailAddress, user.EmailAddress);

                var fields = Builders<UserRegistrationModel>.Projection.Include(p => p.EmailAddress).Include(p => p.Password).Include(p => p.UserType).Include(p => p.IsUserVerified);
                var results = collection.Find(condition).Project<UserRegistrationModel>(fields).ToList().AsQueryable();
                
                if (results.Count() == 1)
                {
                    var data = results.FirstOrDefault();
                    if (data != null)
                    {
                        if (data.Password.Equals(user.Password))
                        {
                            if (data.UserType.Equals("WRITER"))
                            {
                                resultCode.Result = true;
                                resultCode.Message = "Writer Login Successful !!";
                                return resultCode;
                            }
                            if (data.UserType.Equals("EDITOR") && data.IsUserVerified.AsBoolean)
                            {
                                resultCode.Result = true;
                                resultCode.Message = "Editor Login Successful !!";
                                return resultCode;
                            }
                            resultCode.Result = false;
                            resultCode.Message = "Editor not verified yet !!";
                            return resultCode;
                        }
                        resultCode.Result = false;
                        resultCode.Message = "Incorrect Password !!";
                        return resultCode;
                    }
                    resultCode.Result = false;
                    resultCode.Message = "Network Issue, Please try again !!";
                    return resultCode;
                }
                resultCode.Result = false;
                resultCode.Message = "Invalid Email Address";
                return resultCode;
            }
            catch (Exception ex)
            {
                resultCode.Result = false;
                resultCode.Message = "Error occured, Please try again !!";
                return resultCode;
            }
        }

        public ResultCode RegisterUser(UserRegistrationModel modelData)
        {
            ResultCode resultCode = new ResultCode();
            try
            {
                IMongoCollection<UserRegistrationModel> collection =
                    CreateDataConnection(new MongoClient()).GetCollection<UserRegistrationModel>("UserRegistration");
                var condition = Builders<UserRegistrationModel>.Filter.Eq(p => p.EmailAddress, modelData.EmailAddress);
                var fields =
                    Builders<UserRegistrationModel>.Projection.Include(p => p.EmailAddress).Include(p => p.FirstName);
                var results = collection.Find(condition).Project<UserRegistrationModel>(fields).ToList().AsQueryable();
                if (results.Any())
                {
                    // user exists return;
                    resultCode.Result = false;
                    resultCode.Message = "Email Id Exists, Please try again !!";
                    return resultCode;
                }
                else
                {
                    if (modelData.UserType.Equals("WRITER"))
                    {
                        modelData.IsUserVerified = true;
                        resultCode.Message = "Writer registration is successfull";
                    }
                    else
                    {
                        modelData.IsUserVerified = false;
                        resultCode.Message =
                            "Editor registration is successfull!! You will receive a confirmation email !!";
                    }
                    //Insert to DB values

                    var collectionName =
                        CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("UserRegistration");
                    var document = modelData.ToBsonDocument();
                    collectionName.InsertOne(document);

                    resultCode.Result = true;
                    return resultCode;
                }
            }
            catch (Exception ex)
            {
                resultCode.Result = false;
                resultCode.Message = "Error occured, Please try again !!";
                return resultCode;
            }
        }

		public ResultCode VerifyEmail(VerifyEmailDLLModel email)
		{
			ResultCode resultCode = new ResultCode();
			try
			{
				IMongoCollection<UserRegistrationModel> collection =
					CreateDataConnection(new MongoClient()).GetCollection<UserRegistrationModel>("UserRegistration");
				var condition = Builders<UserRegistrationModel>.Filter.Eq(p => p.EmailAddress, email.Email);
				var fields =
					Builders<UserRegistrationModel>.Projection.Include(p => p.EmailAddress);
				var results = collection.Find(condition).Project<UserRegistrationModel>(fields).ToList().AsQueryable();
				if (results.Any())
				{

					resultCode.Result = true;
					resultCode.Message = "Registered User !!";
					return resultCode;
				}
				else
				{
					resultCode.Result = false;
					resultCode.Message = "User does not exists !!";
					return resultCode;
				}

			}
			catch (Exception e)
			{
				resultCode.Result = false;
				resultCode.Message = "Error occured, Please try again !!";
				return resultCode;

			}
		}

		public List<ConnStoryTable> GetAllStories()
		{
			try
			{
				IMongoCollection<ConnStoryTable> collection =
					CreateDataConnection(new MongoClient()).GetCollection<ConnStoryTable>("StoryTable");
				var results = collection.Find(new BsonDocument()).ToList();
				return results;
			}
			catch (Exception ex)
			{
				return null;
			}

		}

		public ResultCode updatepassword(UpdatePasswordModelDLL data, string email)
		{
			ResultCode resultCode = new ResultCode();
			try
			{
				if (data.Password.Equals(data.ConfirmPassword))
				{

					IMongoCollection<BsonDocument> collection =
						CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("UserRegistration");
					var condition = Builders<BsonDocument>.Filter.Eq("EmailAddress", email);
					var update = Builders<BsonDocument>.Update.Set("Password", data.Password)
						.Set("ConfirmPassword", data.ConfirmPassword);
					var res = collection.UpdateOne(condition, update);

					resultCode.Result = true;
					resultCode.Message = "Password Updated Successfully !!";
					return resultCode;
				}
				else
				{

					resultCode.Result = false;
					resultCode.Message = "Password and Confirm Password does not match !!";
					return resultCode;
				}

			}
			catch (Exception e)
			{
				resultCode.Result = false;
				resultCode.Message = "Error occured, Please try again !!";
				return resultCode;

			}

		}
	}
}
