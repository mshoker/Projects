using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace HRPortal.Models
{
    public class ApplyVM
    {
        public List<SelectListItem> Openings { get; set; }
        public Application Application { get; set; }
    }
}