using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.Models
{
    public class BrowseStoryModel
    {
        public List<StoryModel> Stories;
        public List<SelectListItem> GenreValues;
        public List<SelectListItem> TypeValues;
    }
}