using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRPortal.Models
{
    public class Shift
    {
        public DateTime Date { get; set; }
        [Required]
        public int HoursWorked { get; set; }

        public string Day { get; set; }
        public string StartTime { get; set; }

        //Shift()
        //{
        //    Date = DateTime.Now;
        //    Day = Date.ToShortDateString();
        //    StartTime = Date.ToShortTimeString();
        //    HoursWorked = 0;
        //}
    }
}