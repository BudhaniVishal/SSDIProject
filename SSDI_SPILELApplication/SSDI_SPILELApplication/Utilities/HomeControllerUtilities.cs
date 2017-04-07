using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSDI_SPILELApplication.Utilities
{
    public class HomeControllerUtilities
    {
        public static List<SelectListItem> GetGenres()
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
                Text = "Thriller",
                Value = "Thriller"
            });
            return items;
        }

        public static List<SelectListItem> GetTypes()
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
                Text = "Non-Fiction",
                Value = "Non-Fiction"
            });
            items.Add(new SelectListItem
            {
                Text = "Classics",
                Value = "Classics"
            });
            return items;
        }
    }
}