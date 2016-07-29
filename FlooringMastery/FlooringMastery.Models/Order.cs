using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public State OrderState { get; set; }
        public Product OrderProduct { get; set; }
        public decimal Area { get; set; }
        
    }
}
