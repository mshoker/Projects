using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRPortal.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You cannot apply without a resume!")]
        public string Resume { get; set; }
        public ApplicationStatus Status { get; set; }
        public Job Job { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\S+@\S+$", ErrorMessage = "The email address format isn't valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provide the best phone number to reach you")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "The phone number format isn't valid")]
        public string Phone { get; set; }
        //best day of week to reach
        public DaysOfTheWeek PreferredDays { get; set; } 
    }
}
