using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.Models
{
    public class ContributeStoryModel
    {
        public int StoryID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContributorID { get; set; }
        public string ContributionText { get; set; }
        public string ErrorMessage { get; set; }

        public string Scenario { get; set; }
    }
}