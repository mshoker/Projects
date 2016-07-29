using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Models;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class ProductRepositoryTests
    {
        [Test]
        public void GetProductByName()
        {
            IProductRepository repo = ProductRepositoryFactory.Create();

            Product product = repo.GetProductBy("TILE");

            Assert.AreEqual(product.ProductType, "TILE");
        }
    }
}
