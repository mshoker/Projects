using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models
{
    public class Job
    {
        [Required]
        public string Title { get; set; }
        public int JobId { get; set; }
    }
}
