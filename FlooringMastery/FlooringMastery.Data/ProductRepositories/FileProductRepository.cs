using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.Data.ProductRepositories
{
    public class FileProductRepository: IProductRepository
    {
        private const string FILENAME = @"DataFiles\Products.txt";

        public List<Product> LoadProductsFromFile()
        {
            List<Product> products = new List<Product>();

            using (StreamReader sr = File.OpenText(FILENAME))
            {
                string inputLine;
                string[] inputParts;
                List<string> inputLines = new List<string>();

                while ((inputLine = sr.ReadLine()) != null)
                {
                    inputLines.Add(inputLine);
                    try
                    {
                        for (int i = 1; i < inputLines.Count; i++)
                        {
                            inputParts = inputLine.Split(',');
                            Product thisProduct = new Product()
                            {
                                ProductType = inputParts[0].ToUpper(),
                                CostPerSquFoot = decimal.Parse(inputParts[1]),
                                LabourPerSquFoot = decimal.Parse(inputParts[2])
                            };
                            products.Add(thisProduct);
                        }
                    }
                    catch
                    {
                        ErrorLog.Write("Failed to read Product List");
                    }
                }


            }

            return products;

        }

        public Product GetProductBy(string productName)
        {
            return LoadProductsFromFile().FirstOrDefault(p => p.ProductType == productName);
        }

        public List<string> ListAllProductNames()
        {
            List<string> prodNames = new List<string>();
            List<Product> products = LoadProductsFromFile();

            var results = products.Select(p => p.ProductType).Distinct();

            foreach (var prod in results)
            {
                prodNames.Add(prod);
            }

            return prodNames;
        }
    }
}
