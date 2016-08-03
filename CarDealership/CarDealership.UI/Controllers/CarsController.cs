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
    public class CarsController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Vehicle(), RepositoryFactory.Customer(), RepositoryFactory.Request(), RepositoryFactory.Seller());

        // GET: Cars
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Find()
        {
            var cars = ops.AllCars();
            return View(cars);
        }

        public ActionResult Contact(int vehicleId)
        {
            Request newReq  = new Request();
            newReq.Vehicle = ops.GetVehicle(vehicleId);

            return View(newReq);
        }

        [HttpPost]
        public ActionResult Contact(Request request)
        {
            request.Vehicle = ops.GetVehicle(request.Vehicle.VehicleID);
            request.Customer = ops.GetCustomer(request.Customer.CustomerID);
            ops.AddNewRequest(request);
            return View("RequestSent", request);
        }

        public ActionResult RequestSent(Request request)
        {
            return View(request);
        }
    }
}