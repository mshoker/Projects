using System;
using System.Net.Sockets;
using FlooringMastery.BLL;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.UI.Workflows
{
    public class WorkflowEditOrder : IWorkflow
    {
        OrderFunctions manager = new OrderFunctions(OrderRepositoryFactory.Create(), ProductRepositoryFactory.Create(), TaxRepositoryFactory.Create());
        ConsoleIO ci = new ConsoleIO();
        LogFunctions log = new LogFunctions();

       
        public void EditOrder()
        {
            Order orderToEdit = FindOrder();

            if (orderToEdit != null)
            {
                decimal area = orderToEdit.Area;
                Response response = new Response();
                ci.Clear();
                ci.Display($"Current Customer Name: {orderToEdit.CustomerName}");
                string name = ci.PromptString("Enter new information, or leave blank to keep the same:  ");

                bool validState;
                string state;
                do
                {
                    ci.Clear();
                    ci.Display($"Current State:  {orderToEdit.OrderState.StateAbbr}");
                    state = ci.PromptOptions(manager.ListAllStates(), "Select your state from the list, or leave blank to keep the current state:   ").ToUpper();
                    if (state == "")
                    {
                        break;
                    }
                    validState = manager.ValidateStateInput(state);
                } while (!validState);

                bool validProd;
                string product;
                do
                {
                    ci.Clear();
                    ci.Display($"Current Product Type:  {orderToEdit.OrderProduct.ProductType}");
                    product = ci.PromptOptions(manager.ListAllProduts(), "Select a flooring product, or leave blank to keep current selection:    ").ToUpper();
                    if (product == "")
                    {
                        break;
                    }
                    validProd = manager.ValidateProductInput(product);
                } while (!validProd);

                


                bool validInput;
                do
                {
                    ci.Clear();
                    ci.Display($"Current Area:  {orderToEdit.Area}");
                    string input = ci.PromptString("Enter new information, or leave blank to keep the same:  ");
                    if (input == "")
                    {
                        area = -1;
                        break;
                    }
                    
                    validInput = decimal.TryParse(input, out area);
                    if (area < 0)
                    {
                        validInput = false;
                        log.Error($"Negative input attempt for area update on order number: {orderToEdit.OrderNumber}, date: {orderToEdit.OrderDate}, client: {orderToEdit.CustomerName}");

                    }
                } while (!validInput);

                if (name == "" && state == "" && product == "" && area == -1)
                {
                    ci.Clear();
                    ci.Display("No changes have been made");
                    ci.BackToMenu();
                    return;
                }

                response = manager.EditOrder(orderToEdit, name, state, product, area);

                Order newOrder = response.OrderInfo;

                bool confirmed = ConfirmEdit(response.OrderInfo);

                if (confirmed == true)
                {
                    DisplayOrderInfo(response.OrderInfo);
                    ci.Display("Your Changes have been accepted and you order is updated");
                    manager.RemoveOrder(orderToEdit, orderToEdit.OrderDate);
                    manager.AddUpdatedOrder(orderToEdit.OrderDate, newOrder);
                    ci.BackToMenu();
                }
                else
                {
                    ci.Display("Your changes have not been accepted and your order is unchanged");
                    ci.BackToMenu();
                }

            }
            else
            {
                ci.Display("Order does not exist!");
                ci.BackToMenu();
            }
        }

        public bool ConfirmEdit(Order order)
        {
            string choice;
            ci.Clear();
            DisplayOrderInfo(order);

            do
            {
                choice = ci.PromptString("Would you like to keep these changes? Y/N ").ToUpper();
            } while (choice != "Y" && choice != "N");

            if (choice == "N")
            {
                return false;
            }
            else
            {
                return true;
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
            ci.Clear();
            ci.Display("*****UPDATED ORDER*****");
            //order info
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
            EditOrder();
        }
    }
}
