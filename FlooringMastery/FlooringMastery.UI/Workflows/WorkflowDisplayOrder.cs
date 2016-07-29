using System;
using System.Collections.Generic;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowDisplayOrder: IWorkflow
    {
        ConsoleIO ci = new ConsoleIO();
        OrderFunctions manager = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        LogFunctions log = new LogFunctions();

        public void Execute()
        {
            string date = ci.PromptDate();
            string dateString = DateTime.Parse(date).ToString("MMddyyyy");
            List<Order> orders = manager.ListAllOrders(dateString);

            if (orders.Count == 0)
            {
                ci.Clear();
                ci.Display($"No Orders Exist for {date}");
                log.Error("Invalid date for order look up");
                ci.BackToMenu();
                return;
            }

            string userInput;
            do
            {
                ci.Clear();
                ci.Display($"=> Orders for {date}: \n");
                foreach (var order in orders)
                {
                    if (order != null)
                    {

                        DisplayOrderInfo(order);

                    }
                }

                
                int orderNumber;

                userInput =
                    ci.PromptString(
                        "Enter an order number to see more information, or press (Q) to go back to the main menu:   ");
                if (userInput.ToUpper() == "Q")
                {
                    return;
                }
                if (int.TryParse(userInput, out orderNumber) == true)
                {
                    Response orderRequest = manager.GetOrder(date, orderNumber);
                    DisplayOrderAllInfo(orderRequest.OrderInfo);
                }
            } while (userInput.ToUpper() != "Q");

            ci.BackToMenu();
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

        public void DisplayOrderAllInfo(Order order)
        {
            //Console.Clear();
            //order info
            ci.Clear();
            ci.Display("***ORDER***");
            ci.Display($"Order Number: {order.OrderNumber}");
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
            ci.Display("Press Enter to go back");
            Console.ReadLine();
            ci.Clear();

        }

        public void DisplayOrderInfo(Order order)
        {
            ci.Display($"Order Number: {order.OrderNumber}, Customer:   {order.CustomerName}");
            Console.WriteLine();
        }
    }
}
