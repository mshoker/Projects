using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Data.Repositories;
using HRPortal.Models;

namespace HRPortal.Controllers
{
    public class ApplicantsController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Policy(),RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());
        // GET: Applicants
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplyForJob(int compId, int jobId)
        {
            JobVM jvm = new JobVM();
            jvm.company = ops.GetCompany(compId);
            jvm.job = ops.GetJob(jobId);
            jvm.app = new Application();
            jvm.app.Job = ops.GetJob(jobId);

            return View(jvm);
        }

        [HttpPost]
        public ActionResult ApplyForJob(JobVM jvm)
        {
            jvm.job = ops.GetJob(jvm.job.JobId);
            jvm.app.Job = ops.GetJob(jvm.job.JobId);
            jvm.company = ops.GetCompany(jvm.company.CompanyId);
            ops.AddApplication(jvm.company.CompanyId, jvm.app);
            return View("Confirm", jvm);

        }

        public ActionResult SeeCompanies()
        {
            var companies = ops.GetAllCompanies();
            return View(companies);
        }

        public ActionResult SeeJobsByCompany(int id)
        {
            var company = ops.GetCompany(id);
            return View(company);
        }

        public ActionResult SeeJobs()
        {
            var jobs = ops.GetAllJobOpenings();
        
            return View(jobs);
        }

        public ActionResult SeeJobAvailability(int id)
        {
            SearchJobVM sjvm = new SearchJobVM()
            {
                Job = ops.GetJob(id),
                Companies = ops.GetOpenings(id)
            };
            
            return View(sjvm);
        }
    }
}