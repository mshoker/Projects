using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class Model
    {
        public int ModelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Make Make { get; set; }
    }
}
