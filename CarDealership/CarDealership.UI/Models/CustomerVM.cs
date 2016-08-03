using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Models;

namespace CarDealership.UI.Models
{
    public class CustomerVM
    {
        public Customer Customer { get; set; }
        public int CarId { get; set; }
        public string newPhone { get; set; }
        public string newEmail { get; set; }
    }
}