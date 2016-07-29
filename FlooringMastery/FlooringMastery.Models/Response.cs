﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Response
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public Order OrderInfo { get; set; }
    }
}
