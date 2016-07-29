using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Models;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void GetNextNumber()
        {
            IOrderRepository repo = OrderRepositoryFactory.Create();
            int next = repo.GetNextOrderNumber();

            Assert.AreEqual(next, 3);
        }

        //can remove
    }
}
