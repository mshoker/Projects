using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Enums;

namespace CarDealership.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public int SellerID { get; set; }
        public DateTime Year { get; set; }
        public string AdTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public bool IsAvailable { get; set; }
        public CarCondition Condition { get; set; }
        public Model Model { get; set; }
    }
}
