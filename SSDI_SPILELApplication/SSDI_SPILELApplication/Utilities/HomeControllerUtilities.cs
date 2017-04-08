﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSDI_SPILELApplication.Models;

namespace SSDI_SPILELApplication.Utilities
{
    public class HomeControllerUtilities
    {
        internal static List<SelectListItem> GetGenres()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Action/Adventure",
                Value = "Action/Adventure"
            });
            items.Add(new SelectListItem
            {
                Text = "Business",
                Value = "Business"
            });
            items.Add(new SelectListItem
            {
                Text = "Career",
                Value = "Career"
            });
            items.Add(new SelectListItem
            {
                Text = "Comedy",
                Value = "Comedy",
            });
            items.Add(new SelectListItem
            {
                Text = "Detective",
                Value = "Detective"
            });
            items.Add(new SelectListItem
            {
                Text = "Family",
                Value = "Family"
            });
            items.Add(new SelectListItem
            {
                Text = "Ghost",
                Value = "Ghost"
            });
            items.Add(new SelectListItem
            {
                Text = "Mystery",
                Value = "Mystery",
            });
            items.Add(new SelectListItem
            {
                Text = "Thriller/Suspense",
                Value = "Thriller/Suspense"
            });
            return items;
        }

        internal static List<SelectListItem> GetTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Short Stories",
                Value = "Short Stories"
            });
            items.Add(new SelectListItem
            {
                Text = "Article",
                Value = "Article"
            });
            items.Add(new SelectListItem
            {
                Text = "Poetry",
                Value = "Poetry"
            });
            items.Add(new SelectListItem
            {
                Text = "Fiction",
                Value = "Fiction"
            });
            items.Add(new SelectListItem
            {
                Text = "Non Fiction",
                Value = "Non Fiction"
            });
            items.Add(new SelectListItem
            {
                Text = "Classics",
                Value = "Classics"
            });
            return items;
        }

        internal static List<StoryModel> FilterStories(List<StoryModel> storiesAvailable, string selectedGenre, string selectedType)
        {
            try
            {
                if (selectedGenre != string.Empty)
                {
                    storiesAvailable = storiesAvailable.FindAll(x => x.Genre == selectedGenre).ToList();
                }
                if(selectedType != string.Empty)
                {
                    storiesAvailable = storiesAvailable.FindAll(x => x.Type == selectedType).ToList();
                }
                return storiesAvailable;
            }

            catch(Exception ex)
            {
                return storiesAvailable;
            }

            
            
        }
    }
}