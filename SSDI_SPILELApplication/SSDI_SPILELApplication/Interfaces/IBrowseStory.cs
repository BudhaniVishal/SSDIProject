﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBaseAccessLayer.ConnectionClass;
using SSDI_SPILELApplication.Models;


namespace SSDI_SPILELApplication.Interfaces
{
    interface IBrowseCreatorStory
    {
        List<ConnStoryTable> BrowseCreatorStory(String ID);
    }
}