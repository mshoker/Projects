using System;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowMainMenu: IWorkflow
    {
        OrderFunctions manager = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        ConsoleIO ci = new ConsoleIO();
        
        public void Execute()
        {
            StartMenu();
        }

        public void StartMenu()
        {
            bool validInt = false;
            int userChoice;

            do
            {
                do
                {
                    Console.Clear();
                    ci.StartMenuOptions();
                    userChoice = ci.PromptInt("Enter the number corresponding to the action you would like to take:    ");
                    validInt = ValidateUserStartChoice(userChoice);
                } while (!validInt);

                Console.Clear();
                ProcessChoice(userChoice);
            } while (userChoice != 5);
        }

        //create over load prompt in w max and min values to condense code
        public bool ValidateUserStartChoice(int userChoice)
        {
            if (userChoice > 0 && userChoice < 6)
            {
                return true;
            }
            else
            {
                ci.Display("Please select a valid number, 1 - 5");
                return false;
            }
        }

        public void ProcessChoice(int choice)
        {
            IWorkflow action;
            switch (choice)
            {
                case 1:
                    action = new WorkflowDisplayOrder();
                    action.Execute();
                    break;
                case 2:
                    action = new WorkflowAddOrder();
                    action.Execute();
                    break;
                case 3:
                    action = new WorkflowEditOrder();
                    action.Execute();
                    break;
                case 4:
                    action = new WorkflowRemoveOrder();
                    action.Execute();
                    break;
                case 5:
                    action = new WorkflowExit();
                    action.Execute();
                    break;
            }
        }

        public Order FindOrder()
        {

            Console.Clear();
            string queryDate = ci.PromptDate();
            int queryNumber = ci.PromptInt("Enter your order number:   ");

            Order queryOrder = manager.GetOrder(queryDate, queryNumber).OrderInfo;
            if (queryOrder != null)
            {
                queryOrder.OrderDate = queryDate;
            }

            return queryOrder;
        }

    }
}
