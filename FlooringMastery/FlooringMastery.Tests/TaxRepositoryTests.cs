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
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class TaxRepositoryTests
    {
        [Test]
        public void GetStateByAbbr()
        {
            ITaxRepository repo = TaxRepositoryFactory.Create();
            State state = repo.GetTaxBy("OH");
            
            Assert.AreEqual(state.StateAbbr, "OH");
        }

      
    }
}
