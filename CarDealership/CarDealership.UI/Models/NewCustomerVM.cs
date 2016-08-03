using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.UI.Models
{
    public class NewCustomerVM
    {
        public int CarId { get; set; }
        public string Name { get; set; }    
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; set; }
    }
}