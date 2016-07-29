using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;

namespace FlooringMastery.Data.TaxRepositories
{
    public class TaxRepositoryFactory
    {
        public static ITaxRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode.ToUpper())
            {
                case "TEST":
                    return new TestTaxRepo();
                case "PROD":
                    return new FileTaxRepo();
                default:
                    throw new Exception("I don't know that mode");
            }
        }           

    }
}
