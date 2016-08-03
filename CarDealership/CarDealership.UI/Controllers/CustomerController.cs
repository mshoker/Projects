using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using CarDealership.BLL;
using CarDealership.Data;
using CarDealership.Models;
using CarDealership.UI.Models;

namespace CarDealership.UI.Controllers
{
    public class CustomerController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Vehicle(), RepositoryFactory.Customer(), RepositoryFactory.Request(), RepositoryFactory.Seller());

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        //when accessing via customer controller, set carId = 0
        public ActionResult NewAccount(int carId)
        {
            NewCustomerVM ncvm = new NewCustomerVM();
            ncvm.CarId = carId;
            return View(ncvm);
        }

        [HttpPost]
        public ActionResult NewAccount(NewCustomerVM ncvm)
        {
            Customer customer = new Customer();
            customer.Name = ncvm.Name;
            Customer newCust = ops.AddCustomer(customer);
            ops.AddEmail(newCust.CustomerID, ncvm.Email);
            ops.AddPhone(newCust.CustomerID, ncvm.Phone, "default");
            
            CustomerVM cvm = new CustomerVM()
            {
                Customer = newCust,
                CarId = ncvm.CarId,
        };
            return View("AccountSummary", cvm);
        }

        public ActionResult AccountSummary(CustomerVM cvm)
        {
            return View(cvm);
        }

        public ActionResult AddPhone(int customerId, int carId)
        {
            CustomerVM cvm = new CustomerVM()
            {
                Customer = ops.GetCustomer(customerId),
                CarId = carId
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult AddPhone(CustomerVM cvm)
        {
            cvm.Customer = ops.GetCustomer(cvm.Customer.CustomerID);
            ops.AddPhone(cvm.Customer.CustomerID, cvm.newPhone, "default");
            return View("AccountSummary", cvm);
        }

        public ActionResult AddEmail(int customerId, int carId)
        {
            CustomerVM cvm = new CustomerVM()
            {
                Customer = ops.GetCustomer(customerId),
                CarId = carId
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult AddEmail(CustomerVM cvm)
        {
            cvm.Customer = ops.GetCustomer(cvm.Customer.CustomerID);
            ops.AddEmail(cvm.Customer.CustomerID, cvm.newEmail);

            return View("AccountSummary", cvm);
        }
    }
}