using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.Data.ProductRepositories
{
    public class TestProductRepo: IProductRepository
    {
        public static List<Product> products = new List<Product>()
        {
            new Product { ProductType = "TILE", CostPerSquFoot = 12.30m, LabourPerSquFoot = 12.40m},
            new Product {ProductType = "CARPET", CostPerSquFoot = 4.57m, LabourPerSquFoot = 12.40m}
        };

        public Product GetProductBy(string productName)
        {
            return products.FirstOrDefault(p => p.ProductType == productName);
        }

        public List<string> ListAllProductNames()
        {
            List<string>names = new List<string>();
            var prodNames = products.Select(p => p.ProductType);

            foreach(var name in prodNames)
            {
                names.Add(name);
            } 

            return names;
        }
    }
    }

