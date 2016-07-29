using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HRPortal.BLL;
using HRPortal.Data.Repositories;
using HRPortal.Models;

namespace HRPortal.Controllers
{
    public class TimeSheetController : ApiController
    {
        Operations ops = new Operations(RepositoryFactory.Policy(), RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());

        //public List<Company> GetCompanies()
        //{
        //    return ops.GetAllCompanies().ToList();
        //}

        //public List<Employee> GetEmployees()
        //{
        //    List<Employee> employees = new List<Employee>();

        //    foreach (var company in ops.GetAllCompanies())
        //    {
        //        var emps = ops.GetEmployeesByCompany(company.CompanyId);
        //        foreach (var e in emps)
        //        {
        //            employees.Add(e);
        //        }
        //    }

        //    return employees;
        //}

        public List<Shift> GetEntries(int empId)
        {
            return ops.GetEmployee(empId).WorkHistory;
        }

        
    }
}
