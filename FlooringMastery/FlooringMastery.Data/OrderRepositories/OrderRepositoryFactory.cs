using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;

namespace FlooringMastery.Data.OrderRepositories
{
    public class OrderRepositoryFactory
    {
        public static IOrderRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            

            switch (mode.ToUpper())
            {
                case "TEST":
                    return new TestOrderRepo();
                case "PROD":
                    return new FileOrderRepository();
                default:
                    throw new Exception("I don't know that mode");
            }
            
        }
    }
}
