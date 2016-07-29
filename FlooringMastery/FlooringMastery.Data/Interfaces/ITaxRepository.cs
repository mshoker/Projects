using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data.Interfaces
{
    public interface ITaxRepository
    {
        State GetTaxBy(string state);

        List<string> ListAllStates();

    }
}
