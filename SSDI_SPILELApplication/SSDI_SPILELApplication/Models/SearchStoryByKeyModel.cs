using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.Models
{
	public class SearchStoryByKeyModel
	{
		public List<StoryModel> Stories;
		public string searchKey { get; set; }
	}
}