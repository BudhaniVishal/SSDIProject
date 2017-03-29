using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.Models
{
    public class StoryModel
    {
        public String Title { get; set; }
        public String Scenario { get; set; }
        public String Content { get; set; }
        public int StoryID { get; set; }
        public String Type { get; set; }
        public String genre { get; set; }
        public DateTime to { get; set; }
        public DateTime from {get; set;}
        public string MessageString { get; set; }
    }
}