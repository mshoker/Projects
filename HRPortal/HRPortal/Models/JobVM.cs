using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRPortal.Models.Enums;

namespace HRPortal.Models
{
    public class JobVM
    {
        public Job job { get; set; }
        public Company company { get; set; }
        public Application app { get; set; }
        public List<SelectListItem> Statuses { get; }

        public JobVM()
        {
            Statuses = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "Needs To Be Reviewed"
                },
                new SelectListItem()
                {
                    Value = "1",
                    Text = "Rejected"
                },
                new SelectListItem()
                {
                    Value = "2",
                    Text = "Interviewing"
                },
                new SelectListItem()
                {
                    Value = "3",
                    Text = "Accepted"
                }
            };

        }
    }
}