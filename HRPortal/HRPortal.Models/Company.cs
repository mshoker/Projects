using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public List<Job> Openings { get; set; }
        public List<Application> Applications { get; set; }
        public string Name { get; set; }
    }
}
