using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataBaseAccessLayer.ConnectionClass
{
    class CreatorStoryModel
    {
        public ObjectId _id { get; set; }
        public int StoryID { get; set; }
        public string EditorID { get; set; }
    }
}
