using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Data.Repositories;
using HRPortal.Models;
using HRPortal.Models.Enums;

namespace HRPortal.Controllers
{
    public class CompanyController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Policy(), RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());

        // GET: Company
        public ActionResult Index()
        {
            return View("List", ops.ListAllCompanies());
        }

        public ActionResult List()
        {
            return View(ops.ListAllCompanies());
        }

        public ActionResult ListApplications(int compId)
        {
            var company = ops.GetCompany(compId);
            return View(company);
        }

        public ActionResult ChangeApplicationStatus(int applicationIndex, int companyId)
        {
            var comp = ops.GetCompany(companyId);
            var application = comp.Applications[applicationIndex];
            JobVM jvm = new JobVM()
            {
                app = application,
                company = comp,
                job = application.Job
            };
            return View(jvm);
        }

        [HttpPost]
        public ActionResult ChangeApplicationStatus(JobVM jvm)
        {
            ops.EditStatus(jvm.company.CompanyId, jvm.app.ApplicationId, jvm.app.Status);
            if (jvm.app.Status == ApplicationStatus.Accepted)
            {
                //TODO: create check for employee duplicate
                //add to list of employees
                Employee newEmployee = new Employee()
                {
                    Employer = ops.GetCompany(jvm.company.CompanyId),
                    FirstName = jvm.app.FirstName,
                    LastName = jvm.app.LastName,
                    Job = ops.GetJob(jvm.job.JobId),
                    Phone = jvm.app.Phone
                };

                ops.AddNewEmployee(newEmployee);
            }
            var company = ops.GetCompany(jvm.company.CompanyId);
            return View("ListApplications", company);
        }

        public ActionResult ManageJobs(int compId)
        {
            Company company = ops.GetCompany(compId);
            return View(company);
        }

        public ActionResult DeleteOpening(int compId, int jobId)
        {
            Company company = ops.GetCompany(compId);
            ops.RemoveOpening(compId, jobId);
            return View("ManageJobs", company);
        }

        public ActionResult AddOpening(int compId)
        {
             
            JobVM jvm = new JobVM();
            jvm.company = ops.GetCompany(compId);
            jvm.job = new Job();
            return View(jvm);
        }

        [HttpPost]
        public ActionResult AddOpening(JobVM jvm)
        {
            ops.AddJobOpening(jvm.job.Title, jvm.company.CompanyId);
            jvm.company = ops.GetCompany(jvm.company.CompanyId);
            return View("ManageJobs", jvm.company);
        }

        public ActionResult EmployeeInfo(int compId)
        {
            Company company = ops.GetCompany(compId);
            List<Employee> employees = ops.GetEmployeesByCompany(compId).OrderBy(e => e.LastName).ToList();

            EmployeeVM evm = new EmployeeVM()
            {
                Company = company,
                Employees = employees
            };

            return View(evm);
        }

        public ActionResult WorkHistory(int compId, int empId)
        {
            Employee employee = ops.GetEmployeesByCompany(compId).First(e => e.Id == empId);

            return View(employee);
        }
    }
}