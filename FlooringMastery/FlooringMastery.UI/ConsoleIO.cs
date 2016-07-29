using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI
{
    public class ConsoleIO
    {
        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public string PromptString(string message)
        {
            Display(message);
            return Console.ReadLine();
        }

        public int PromptInt(string message)
        {
            int userInt;
            bool isValid = false;
            do
            {
                Display(message);
                string userInput = Console.ReadLine();

                isValid = int.TryParse(userInput, out userInt);

                if (!isValid)
                {
                    Display("Must be a number!");
                }
            } while (isValid == false);

            return userInt;
        }

        public string PromptDate()
        {
            bool validDate = false;
            DateTime date;
            string queryDate;
            do
            {
                Console.Clear();
                queryDate = PromptString("Enter your order date:  ");

                validDate = DateTime.TryParse(queryDate, out date);
            } while (validDate == false);

            return queryDate;
        }
        
        public void StartMenuOptions()
        {
            Display("*********************************************************");
            Display("*                Flooring Program");
            Console.WriteLine();
            Display("*    1. Display Orders");
            Console.WriteLine();
            Display("*    2. Add an order");
            Console.WriteLine();
            Display("*    3. Edit an Order");
            Console.WriteLine();
            Display("*    4. Remove an Order");
            Console.WriteLine();
            Display("*    5. Quit");
            Console.WriteLine();
            Display("*");
            Console.WriteLine();
            Display("*********************************************************");
        }
        
        public void BackToMenu()
        {
            Display("Press Enter to Return to Main Menu");
            Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public string PromptOptions(List<string> options, string message)
        {
            Display(message);
            foreach (var option in options)
            {
                Display(option);
            }

            Console.Write("Choice:  ");
            return Console.ReadLine();
        }

        
    }
}
