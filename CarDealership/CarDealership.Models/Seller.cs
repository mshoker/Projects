using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class Seller
    {
        public int SellerID { get; set; }
        public string Name { get; set; }

        public List<Request> Requests { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
