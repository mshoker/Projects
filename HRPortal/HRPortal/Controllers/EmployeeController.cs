using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Data.Repositories;
using HRPortal.Models;

namespace HRPortal.Controllers
{
    public class EmployeeController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Policy(), RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeVM evm = new EmployeeVM()
            {
                CompaniesSelect = new List<SelectListItem>(),
                EmployeesSelect = new List<SelectListItem>(),
                Companies = ops.GetAllCompanies().ToList(),
                Employees = new List<Employee>(),
                Company = new Company(),
                Employee = new Employee()
            };

            //initialize select list items
            foreach (var company in ops.GetAllCompanies())
            {
                evm.CompaniesSelect.Add(new SelectListItem()
                {
                    Text = company.Name,
                    Value = company.CompanyId.ToString()
                });
                var workers = ops.GetEmployeesByCompany(company.CompanyId);
                foreach (var worker in workers)
                {
                    evm.EmployeesSelect.Add(new SelectListItem()
                    {
                        Text = worker.FirstName + " " + worker.LastName,
                        Value = worker.Id.ToString()
                    });

                    evm.Employees.Add(worker);
                }
            }
            //select their company
            //then select their name
            //then view timesheet and be prompted to add or edit or delete
            return View(evm);
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult TimeEntries()
        {
            return View();
        }

        public ActionResult AddTimeEntry(int compId, int empId)
        {
            return View();
        }

        public ActionResult EditTimeEntry()
        {
            return View();
        }

        public ActionResult DeleteTimeEntry()
        {
            return View();
        }
    }
}