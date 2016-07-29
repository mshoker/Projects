using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Models
{
    public class EmployeeVM
    {
        public Company Company { get; set; }
        public Employee Employee { get; set; }
        public List<SelectListItem> EmployeesSelect { get; set; }
        public List<SelectListItem> CompaniesSelect { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Company> Companies { get; set; }
    }
}