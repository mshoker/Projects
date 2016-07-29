using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.Data.TaxRepositories
{
    public class TestTaxRepo : ITaxRepository
    {
        private static List<State> _states = new List<State>()
        {
            new State { StateAbbr="OH", StateName="Ohio", TaxRate=0.065M },
            new State {StateAbbr = "MI", StateName = "Michigan", TaxRate = 0.05M}
        };

        public State GetTaxBy(string state)
        {
            return _states.FirstOrDefault(s => s.StateAbbr == state);
        }

        public List<string> ListAllStates()
        {
            List<string> states = new List<string>();
            var results = _states.Select(s => s.StateAbbr);
            foreach (var s in results)
            {
                states.Add(s);
            }

            return states;
        }
    }
}
