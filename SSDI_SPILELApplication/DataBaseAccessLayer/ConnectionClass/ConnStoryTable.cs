using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataBaseAccessLayer.ConnectionClass
{
    public class ConnStoryTable
    {
            public ObjectId _id { get; set; }
            public String Title { get; set; }
            public String Scenario { get; set; }
            public String Content { get; set; }
            public int StoryID { get; set; }
            public String Type { get; set; }
            public String Genre { get; set; }
            public string To { get; set; }
            public string From { get; set; }
    }
}
