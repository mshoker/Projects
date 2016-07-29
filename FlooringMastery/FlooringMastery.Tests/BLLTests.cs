using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;
using FlooringMastery.UI;
using FlooringMastery.UI.Workflows;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class BLLTests
    {
        OrderFunctions ops = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        IOrderRepository oRepo = OrderRepositoryFactory.Create();

        //can add
        [TestCase("test", "TILE","OH", 80 )]
        public void CanAddOrder(string name, string product, string stateAbbr, decimal area)
        {
           bool result = ops.AddOrder(name, product, stateAbbr, area).IsSuccessful;

            Assert.IsTrue(result);
        }
        
        //can edit
        [Test]
        public void CanEditOrder()
        {
            Order toUpdate = oRepo.GetOrderByDateandNumber("6/28/2016", 1);

            bool result = ops.EditOrder(toUpdate, "test", "OH", "TILE", 900m).IsSuccessful;

            Assert.IsTrue(result);
        }

        //can get
        [TestCase("6/28/2016", 1)]
        public void CanGetOrder(string date, int number)
        {
            bool result = ops.GetOrder(date, number).IsSuccessful;

            Assert.IsTrue(result);
        }

        [TestCase("tile", true)]
        [TestCase("CARPET", true)]
        [TestCase("Carpet", true)]
        [TestCase("notaproduct", false)]
        public void ValidProduct(string input, bool expected)
        {
            bool result = ops.ValidateProductInput(input);

            Assert.AreEqual(result, expected);
        }

        [TestCase("Oh", true)]
        [TestCase("OH", true)]
        [TestCase("oh", true)]
        [TestCase("notastate", false)]
        public void ValidState(string input, bool expected)
        {
            bool result = ops.ValidateStateInput(input);

            Assert.AreEqual(result, expected);
        }
    }
}
