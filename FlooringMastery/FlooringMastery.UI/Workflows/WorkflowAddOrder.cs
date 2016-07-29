using System;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowAddOrder: IWorkflow
    {
        OrderFunctions manager = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        ConsoleIO ci = new ConsoleIO();
        LogFunctions log = new LogFunctions();
        public void CreateOrder()
        {
            Response response = new Response();
            string cltName;
            string cltProduct;
            string cltStateAbbr;
            decimal cltArea;
            bool validInput = false;


            do
            {
                Console.Clear();
                cltName = ci.PromptString("Enter your name:");
            } while (string.IsNullOrEmpty(cltName));

            bool validProduct;
            do
            {
                Console.Clear();
                cltProduct = ci.PromptOptions(manager.ListAllProduts(), "Select your flooring product:  ").ToUpper();
                validProduct = manager.ValidateProductInput(cltProduct);
            } while (!validProduct);

            bool validState;
            do
            {
                Console.Clear();
                cltStateAbbr = ci.PromptOptions(manager.ListAllStates(), "Select your state:   ").ToUpper();
                validState = manager.ValidateStateInput(cltStateAbbr);
            } while (!validState);

            bool validArea;
            do
            {
                Console.Clear();
                string cltAreaInput = ci.PromptString("Enter the area:");
                validArea = decimal.TryParse(cltAreaInput, out cltArea);
                if (cltArea < 0)
                {
                    validArea = false;
                    log.Error($"Negative input attempt for area during create order, client: {cltName}");

                }

            } while ( validArea == false);

            response = manager.AddOrder(cltName, cltProduct, cltStateAbbr, cltArea);

            if (!response.IsSuccessful)
            {
                ConfirmOrder(response);
                ci.Display(response.Message);
                Console.ReadLine();
            }
            else
            {
                ConfirmOrder(response);
            }
        }

        public bool ConfirmOrder(Response response)
        {
            string cltConfirmation;
            DisplayOrderInfo(response.OrderInfo);
            do
            {
                cltConfirmation = ci.PromptString("Are you sure you want to create this order? Y/N").ToUpper();
            } while (cltConfirmation != "Y" && cltConfirmation != "N");

            if (cltConfirmation == "N")
            {
                ci.Clear();
                manager.CancelOrder(response.OrderInfo, response.OrderInfo.OrderDate);
                ci.Display("Order cancelled");
                ci.BackToMenu();
                return false;
            }
            else
            {
                ci.Clear();
                ci.Display("Your order is confirmed!");
                ci.BackToMenu();
                return true;
            }
        }

        public void DisplayOrderInfo(Order order)
        {
            //Console.Clear();
            order.OrderDate = DateTime.Today.ToShortDateString();
            //order info
            ci.Clear();
            ci.Display("***ORDER***");
            ci.Display($"Order Number: {order.OrderNumber}");
            ci.Display($"Order Date:   {order.OrderDate}");
            ci.Display($"Customer: {order.CustomerName}");
            Console.WriteLine();
            //product info
            ci.Display($"Product:  {order.OrderProduct.ProductType}");
            ci.Display($"Product Material Price per Square Foot:   {order.OrderProduct.CostPerSquFoot}");
            ci.Display($"Product Labour Price per Square Foot: {order.OrderProduct.LabourPerSquFoot}");

            //create total costs method
            Console.WriteLine();
            ci.Display($"Total Area:   {order.Area} sq ft");
            ci.Display($"Total Labour Costs:   {manager.TotalLabourCosts(order)}");
            ci.Display($"Total Material Costs: {manager.TotalMaterialCosts(order)}");

            //tax info
            Console.WriteLine();
            ci.Display($"Customer State:   {order.OrderState.StateName}");
            ci.Display($"State Tax Rate:   {order.OrderState.TaxRate}");

            Console.WriteLine();
            ci.Display($"TOTAL COST:  {manager.TotalCosts(order)}");
            Console.WriteLine();
            Console.WriteLine();

        }

        public void Execute()
        {
            CreateOrder();
        }
    }
}
