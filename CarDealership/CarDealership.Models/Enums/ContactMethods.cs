using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Enums
{
    [Flags]
    public enum ContactMethods
    {
        None = 0,
        Phone = 1,
        Email = 2,
        InPerson = 4
    }
}
