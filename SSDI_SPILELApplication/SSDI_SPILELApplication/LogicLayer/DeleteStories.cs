using DataBaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDI_SPILELApplication.LogicLayer
{
    public class DeleteStories
    {
        public bool DeleteRestContributedStories(int storyID, string ContributorID)
        {
            DatabaseAccess objDatabaseAccess = new DatabaseAccess();
            bool result = objDatabaseAccess.DeleteRestContributedStories(storyID, ContributorID);
            return result;
        }
    }
}