using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.BLL;
using CarDealership.Data;

namespace CarDealership.UI.Controllers
{
    public class AdminController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Vehicle(), RepositoryFactory.Customer(), RepositoryFactory.Request(), RepositoryFactory.Seller());


        // GET: Admin
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ListBuyers()
        {
            var customers = ops.GetAll();
            return View(customers);
        }
    }
}