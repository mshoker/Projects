using System;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowRemoveOrder: IWorkflow
    {
        OrderFunctions manager = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        ConsoleIO ci = new ConsoleIO();
        
        public void RemoveOrder()
        {
            string userInput;
            Order orderToCancel = FindOrder();
            if (orderToCancel == null)
            {
                ci.Display("Order does not exist!");
                ci.BackToMenu();
            }
            else
            {
                do
                {
                    ci.Clear();
                    DisplayOrderInfo(orderToCancel);
                    userInput = ci.PromptString("Are you sure you want to cancel this order? Y/N").ToUpper();
                } while (userInput != "Y" && userInput != "N");

                switch (userInput)
                {
                    case "Y":
                        ci.Clear();
                        manager.RemoveOrder(orderToCancel, orderToCancel.OrderDate);
                        ci.Display("Order Cancelled!");
                        Console.ReadLine();
                        break;
                    case "N":
                        ci.Clear();
                        ci.Display("Your order has NOT been cancelled");
                        ci.Display("Press enter to return to the main menu");
                        Console.ReadLine();
                        break;
                    default:
                        ci.Display("Something went wrong");
                        Console.ReadLine();
                        break;
                }
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

        public void DisplayOrderInfo(Order order)
        {
            //Console.Clear();
            //order info
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
        }

        public void Execute()
        {
            RemoveOrder();
        }
    }
}
