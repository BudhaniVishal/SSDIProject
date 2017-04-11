using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.Models
{
    public class StorySuggestionsModel
    {
		public StoryModel CurrentStory;
        public List<SuggestionModel> Suggestions;
    }
}