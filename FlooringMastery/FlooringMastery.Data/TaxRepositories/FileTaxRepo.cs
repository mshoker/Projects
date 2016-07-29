using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Models;

namespace FlooringMastery.Data.TaxRepositories
{
    public class FileTaxRepo: ITaxRepository
    {
        private const string FILENAME = @"DataFiles\Taxes.txt";

        private  List<State> LoadStatesFromFile()
        {
            List<State> stateTaxList = new List<State>();

            using (StreamReader sr = File.OpenText(FILENAME))
            {
                string inputLine;
                string[] inputParts;
                List<string> inputLines = new List<string>();

                while ((inputLine = sr.ReadLine()) != null)
                {
                    try
                    {
                        inputLines.Add(inputLine);

                        for (int i = 1; i < inputLines.Count; i++)
                        {
                            inputParts = inputLines[i].Split(',');
                            State thisState = new State()
                            {
                                StateAbbr = inputParts[0].ToUpper(),
                                StateName = inputParts[1].ToUpper(),
                                TaxRate = decimal.Parse(inputParts[2])
                            };

                            stateTaxList.Add(thisState);
                        }
                    }
                    catch 
                    {
                        ErrorLog.Write($"Failed to rewrite state list");
                    }
                    
                }
            }
            return stateTaxList;
        }

        public State GetTaxBy(string state)
        {
            return LoadStatesFromFile().FirstOrDefault(s => s.StateAbbr == state);
        }

        public List<string> ListAllStates()
        {
            List<string> stateAbbreviations = new List<string>();
            List<State> states = LoadStatesFromFile();
            var results = states.Select(s => s.StateAbbr).Distinct();
            foreach (var r in results)
            {
                stateAbbreviations.Add(r);
            }


            return stateAbbreviations;
        }
    }
}
