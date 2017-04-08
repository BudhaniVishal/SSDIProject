using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataBaseAccessLayer.ConnectionClass
{
    class ContributorStoryModel
    {
        public ObjectId _id { get; set; }
        public int StoryID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContributorID { get; set; }
    }
}
