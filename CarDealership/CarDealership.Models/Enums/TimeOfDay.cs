using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Enums
{
    [Flags]
    public enum TimeOfDay
    {
        None = 0,
        Morning = 1,
        Afternoon = 2,
        Evening = 4
    }
}
