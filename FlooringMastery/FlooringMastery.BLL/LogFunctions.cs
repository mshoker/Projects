using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;

namespace FlooringMastery.BLL
{
    public class LogFunctions
    {
        public void Error(string message)
        {
            ErrorLog.Write(message);
        }
    }
}
