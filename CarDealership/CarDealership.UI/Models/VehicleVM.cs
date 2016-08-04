using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;

namespace CarDealership.UI.Models
{
    public class VehicleVM
    {
        public Vehicle Vehicle { get; set; }
        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> Models { get; set; }
        public List<SelectListItem> Condition { get; set; }

    }
}