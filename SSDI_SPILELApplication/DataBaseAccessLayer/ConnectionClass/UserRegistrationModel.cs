using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataBaseAccessLayer.ConnectionClass
{
    public class UserRegistrationModel
    {
        public ObjectId _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserType { get; set; }
        public BsonBoolean IsUserVerified { get; set; }
    }
}
