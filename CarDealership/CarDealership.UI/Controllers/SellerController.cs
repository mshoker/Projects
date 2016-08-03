using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.BLL;
using CarDealership.Data;
using CarDealership.Models;

namespace CarDealership.UI.Controllers
{
    public class SellerController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Vehicle(), RepositoryFactory.Customer(), RepositoryFactory.Request(), RepositoryFactory.Seller());

        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewSeller()
        {
            Seller newSeller = new Seller();
            return View(newSeller);
        }

        [HttpPost]
        public ActionResult NewSeller(Seller newSeller)
        {
            Seller seller = ops.AddSeller(newSeller);
            return View("Account", seller);
        }

        public ActionResult Existing(int id)
        {
            Seller seller = ops.GetSeller(id);
            return View(seller);
        }

        public ActionResult Account(int id)
        {
            Seller seller = ops.GetSeller(id);
            return View(seller);
        }

    }
}