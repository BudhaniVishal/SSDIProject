using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataBaseAccessLayer.ConnectionClass {
	public class SuggestionTable {
		public ObjectId _id { get; set; }
		public String Content { get; set; }
        public int SuggestionID { get; set; }
		public int OwningStoryID { get; set; }
		public DateTime DatePosted { get; set; }
	}
}
