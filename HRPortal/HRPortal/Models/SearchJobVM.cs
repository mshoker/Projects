using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HRPortal.Models
{
    public class SearchJobVM
    {
        public Job Job { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}