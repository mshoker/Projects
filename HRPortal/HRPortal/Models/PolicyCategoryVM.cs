using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Models
{
    public class PolicyCategoryVM
    {
        public List<SelectListItem> Categories { get; set; }
        public List<Policy> Policies { get; set; }
        public string Origin { get; set; }

        public PolicyCategoryVM()
        {
            Categories = new List<SelectListItem>();
            Policies = new List<Policy>();
        }

        public void SetCategoryItems(List<string> categories)
        {
            Categories = new List<SelectListItem>();
            for (int i = 0; i < categories.Count(); i++)
            {
                Categories.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = categories[i]
                });
            }
        }
    }
}