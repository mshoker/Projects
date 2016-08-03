using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.CustomerRepositories;
using CarDealership.Data.Interfaces;
using CarDealership.Data.RequestRepositories;
using CarDealership.Data.SellerRepositories;
using CarDealership.Data.VehicleRepository;

namespace CarDealership.Data
{
    public class RepositoryFactory
    {
        public static ICustomerRepo Customer()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "TEST":
                    return new CustomersMOCK();
                case "PROD":
                    throw new NotImplementedException();
                case "DATABASE":
                    return new CustomersDB();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static IVehicleRepo Vehicle()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "DATABASE":
                    return new VehicleDB();
                case "TEST":
                    return new VehiclesMOCK();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static IRequestRepo Request()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "DATABASE":
                    return new RequestsDB();
                case "TEST":
                    return new RequestsMOCK();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static ISellerRepo Seller()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "DATABASE":
                    return new SellerDB();
                case "TEST":
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }
    }
}
