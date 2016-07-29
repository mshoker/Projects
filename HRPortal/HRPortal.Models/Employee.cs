using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public Company Employer { get; set; }
        public DateTime HireDate { get; set; }
        public List<Shift> WorkHistory { get; set; }
        public Job Job { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

    }
}