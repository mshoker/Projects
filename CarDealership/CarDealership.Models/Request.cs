using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Enums;

namespace CarDealership.Models
{
    public class Request
    {
        public int RequestID { get; set; }
        public Customer Customer  { get; set; }
        public Vehicle Vehicle { get; set; }
        public RequestStatus Status { get; set; }
        public TimeOfDay BestTimeToCall { get; set; }
        public ContactMethods BestContactMethod { get; set; }
        public DaysOfTheWeek PreferredDays { get; set; }
        //make nullable
        public string AdditionalNotes { get; set; }
    }
}
