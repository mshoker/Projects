using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Data.Repositories;

namespace HRPortal.Controllers
{
    public class ExternalController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Policy(), RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());

        // GET: External
        public ActionResult Index()
        {
            //list all companies
            //
            return View();
        }
    }
}