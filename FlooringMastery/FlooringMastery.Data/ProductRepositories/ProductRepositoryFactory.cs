using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;

namespace FlooringMastery.Data.ProductRepositories
{
    public class ProductRepositoryFactory
    {
        public static IProductRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            
            switch (mode.ToUpper())
            {
                case "TEST":
                    return new TestProductRepo();
                case "PROD":
                    return new FileProductRepository();
                default:
                    throw new Exception("I don't know that mode");
            }

        }
    }
}
