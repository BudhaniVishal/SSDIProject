using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.Models
{
    public class SuggestionModel
    {
        public String Content { get; set; }
        public int SuggestionID { get; set; }
		public int OwningStoryID { get; set; }
		public DateTime DatePosted { get; set; }
    }
}