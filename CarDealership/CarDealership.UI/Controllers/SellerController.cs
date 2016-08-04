using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.BLL;
using CarDealership.Data;
using CarDealership.Models;
using CarDealership.Models.Enums;
using CarDealership.UI.Models;

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

        [HttpPost]
        public ActionResult Index(int accountId)
        {
            
            return RedirectToAction("Account", "Seller",new {id = accountId});
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

        public ActionResult NewVehicle(int sellerId)
        {
            Vehicle vehicle = new Vehicle()
            {
                SellerID = sellerId
            };

            VehicleVM vvm = new VehicleVM();
            vvm.Vehicle = vehicle;
            vvm.Makes = new List<SelectListItem>();
            vvm.Models = new List<SelectListItem>();
            //set drop down list items
            foreach(var make in ops.GetAllMakes())
            {
                vvm.Makes.Add(new SelectListItem()
                {
                    Value = make.MakeID.ToString(),
                    Text = make.Name
                });
            }

            foreach (var model in ops.GetAllModels())
            {
                vvm.Models.Add(new SelectListItem()
                {
                    Value = model.ModelID.ToString(),
                    Text = model.Name
                });
            }
            Array conditions = Enum.GetValues(typeof(CarCondition));
            List<SelectListItem> selectConditions = new List<SelectListItem>();

            for(int i = 0; i < conditions.Length -1; i++)
            {
                selectConditions.Add(new SelectListItem()
                {
                    Text = Enum.GetName(typeof(CarCondition), i),
                    Value = i.ToString()
                });
            }

            vvm.Condition = selectConditions;

            return View(vvm);
        }

        [HttpPost]
        public ActionResult NewVehicle(VehicleVM vvm)
        {

            ops.AddVehicle(vvm.Vehicle);
            Seller seller = ops.GetSeller(vvm.Vehicle.SellerID);

            return View("Account", seller);
        }

    }
}