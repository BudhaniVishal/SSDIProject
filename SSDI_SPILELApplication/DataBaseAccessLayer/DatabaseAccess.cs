﻿using System;
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
	public class DatabaseAccess : IDatabaseAccess
	{
		//private static string dataBaseName = ConfigurationManager.AppSettings["Database"];
		private static string dataBaseName = "spielDB";
		public ResultCode CreateStory(ConnStoryTable story, string username)
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

                var Collect = CreateDataConnection(new MongoClient())
                    .GetCollection<BsonDocument>("CreatorStoryTable");
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
                    modelData.IsUserVerified = true;
                    
                    //Insert to DB values

                    var collectionName =
						CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("UserRegistration");
					var document = modelData.ToBsonDocument();
					collectionName.InsertOne(document);

					resultCode.Result = true;
                    resultCode.Message = "Registration is successfull !!";
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
                var i = 0;
                foreach (var story in results)
                {
                    var tableCollection2 = CreateDataConnection(new MongoClient())
                    .GetCollection<CreatorStoryModel>("CreatorStoryTable");
                    var fields = Builders<CreatorStoryModel>.Projection.Include(p => p.EditorID);
                    var condition2 = Builders<CreatorStoryModel>.Filter.Eq(p => p.StoryID, story.StoryID);
                    var editorID = tableCollection2.Find(condition2).Project<CreatorStoryModel>(fields).ToList();
                    if (editorID.Count==0 || editorID[0].EditorID==null) { i++; continue; }
                    else { results[i].EditorID = editorID[0].EditorID; }
                    i++;
                }
                return (results != null) ? results : new List<ConnStoryTable>();
			}
			catch (Exception ex)
			{
				return new List<ConnStoryTable>();
			}

		}

		public ConnStoryTable GetStoryByID(int id) {
			try {
				IMongoCollection<ConnStoryTable> collection =
					CreateDataConnection(new MongoClient()).GetCollection<ConnStoryTable>("StoryTable");
				var results = collection.Find(new BsonDocument()).ToList();
				for (int i = 0; i < results.Count; i++) {
					if (results[i].StoryID == id) {
						return results[i];
					}
				}

				//if we're here, there were no stories with this id
				return new ConnStoryTable();
			}
			catch (Exception ex) {
				return new ConnStoryTable();
			}

		}


		public ResultCode UpdatePassword(UpdatePasswordModelDLL data , string email)
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

        public List<ConnStoryTable> BrowseCreatedStories(string username)
        {   
            List<ConnStoryTable> crtrStoryListObj = new List<ConnStoryTable>();
            ConnStoryTable crtrStoryObj = new ConnStoryTable();
            try
            {
                var tableCollection = CreateDataConnection(new MongoClient())
                    .GetCollection<CreatorStoryModel>("CreatorStoryTable");
                var condition = Builders<CreatorStoryModel>.Filter.Eq(p => p.EditorID, username);
                var results = tableCollection.Find(condition).ToList().AsQueryable();
                if (results == null)
                {
                    crtrStoryListObj[0].MessageString = "Data is null";
                }
                else { 
					foreach (var story in results)
					{
						var tableCollection2 = CreateDataConnection(new MongoClient())
						.GetCollection<ConnStoryTable>("StoryTable");
						var fields = Builders<ConnStoryTable>.Projection.Include(p => p.Title).Include(p => p.Content).Include(p => p.StoryID).Include(p => p.Genre).Include(p => p.Scenario);
						var condition2 = Builders<ConnStoryTable>.Filter.Eq(p => p.StoryID, story.StoryID);
						var StoryResults = tableCollection2.Find(condition2).Project<ConnStoryTable>(fields).ToList();
						for (int i = 0; i < StoryResults.Count(); i++)
						{
							crtrStoryListObj.Add(StoryResults[i]);
						}
					}
                }
                return crtrStoryListObj;
           
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<ContributorStoryModel> BrowseContributedStoriesForEditor(int storyID)
        {
            List<ContributorStoryModel> cntrStoryObj = new List<ContributorStoryModel>();
            try
            {
                var tableCollection = CreateDataConnection(new MongoClient())
                    .GetCollection<ContributorStoryModel>("ContributorStoryTable");
                var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, storyID );
                var results = tableCollection.Find(condition).ToList().AsQueryable();
                foreach (var story in results)
                {
                    cntrStoryObj.Add(story);
                }
                return cntrStoryObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DeleteRestContributedStories(int storyID, string ContributorID)
        {
            List<ContributorStoryModel> cntrStoryObj = new List<ContributorStoryModel>();
            try
            {
                bool result = false;
                var tableCollection = CreateDataConnection(new MongoClient())
                    .GetCollection<ContributorStoryModel>("ContributorStoryTable");
                var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.StoryID, storyID);
                var results = tableCollection.Find(condition).ToList().AsQueryable();
                foreach (var story in results)
                {
                    if (story.ContributorID.Equals(ContributorID))
                    {
                        var StoryCollection = GetStoryByID(storyID);
                        StoryCollection.Content += "\r\n" +story.Content;
                        var collection = CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("StoryTable");
                        var filter = Builders<BsonDocument>.Filter.Eq("StoryID", story.StoryID);
                        var update = Builders<BsonDocument>.Update.Set("Content", StoryCollection.Content);
                        var Updateone = collection.UpdateOneAsync(filter, update);
                    }

                    var story_id = story.StoryID;
                    var Deleteone = tableCollection.DeleteOne(
                        Builders<ContributorStoryModel>.Filter.Eq("StoryID", story_id));
                    if (Deleteone != null) { result = true; }


                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        

        public List<SuggestionTable> BrowseSuggestions(int story_id) {
			List<SuggestionTable> crtrSuggestionListObj = new List<SuggestionTable>();
			SuggestionTable crtrSuggestionObj = new SuggestionTable();
			try {
				var tableCollection = CreateDataConnection(new MongoClient())
					.GetCollection<SuggestionTable>("SuggestionTable");
				var condition = Builders<SuggestionTable>.Filter.Eq(p => p.OwningStoryID, story_id);
				var results = tableCollection.Find(condition).ToList().AsQueryable();
				foreach (var suggestion in results) {
					crtrSuggestionListObj.Add(suggestion);
				}
				return crtrSuggestionListObj;
			}
			catch (Exception ex) {
				return null;
			}
		}

		public SuggestionTable GetSuggestionByID(int suggestion_id) {
			try {
				IMongoCollection<SuggestionTable> collection =
					CreateDataConnection(new MongoClient()).GetCollection<SuggestionTable>("SuggestionTable");
				var results = collection.Find(new BsonDocument()).ToList();
				for (int i = 0; i < results.Count; i++) {
					if (results[i].SuggestionID == suggestion_id) {
						return results[i];
					}
				}

				//if we're here, there were no stories with this id
				return null;
			}
			catch (Exception ex) {
				return null;
			}
		}

		public List<ContributorStoryModel> BrowseContributorStories(string username)
        {
            List<ContributorStoryModel> cntrStoryObj = new List<ContributorStoryModel>();
            try
            {
                var tableCollection = CreateDataConnection(new MongoClient())
                    .GetCollection<ContributorStoryModel>("ContributorStoryTable");
                var condition = Builders<ContributorStoryModel>.Filter.Eq(p => p.ContributorID, username);
                var results = tableCollection.Find(condition).ToList().AsQueryable();
                foreach (var story in results)
                {
                    cntrStoryObj.Add(story);
                }
                return cntrStoryObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

	    public bool SaveContributionForStory(ContributorStoryModel modelData)
	    {
	        bool result = false;
	        try
	        {
	            if (modelData.Content != string.Empty)
	            {
	                modelData.Title = GetTitle(modelData.StoryID);
	                var collectionName =
	                    CreateDataConnection(new MongoClient()).GetCollection<BsonDocument>("ContributorStoryTable");
	                var document = modelData.ToBsonDocument();
	                collectionName.InsertOne(document);
	                result = true;
	            }
	            return result;
	        }
	        catch (Exception ex)
	        {
	            return false;
	        }

	    }

	    private string GetTitle(int storyID)
	    {
	        string title = string.Empty;
	        IMongoCollection<ConnStoryTable> collection = CreateDataConnection(new MongoClient())
	            .GetCollection<ConnStoryTable>("StoryTable");
	        var condition = Builders<ConnStoryTable>.Filter.Eq(p => p.StoryID, storyID);

	        var fields = Builders<ConnStoryTable>.Projection.Include(p => p.Title).Include(p => p.StoryID);
	        var results = collection.Find(condition).Project<ConnStoryTable>(fields).ToList().AsQueryable();
	        if (results.Any())
	        {
	            var data = results.FirstOrDefault();
	            title = data.Title;
	        }
	        return title;
	    }
    }
}
