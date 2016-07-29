using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.Data.OrderRepositories
{
    public class FileOrderRepository : IOrderRepository
    {
        private const string FILENAME = @"DataFiles\Orders\Orders_";
        private const string FILEEXT = @".txt";

        IProductRepository prodRepo = ProductRepositoryFactory.Create();
        ITaxRepository stateRepo = TaxRepositoryFactory.Create();
        
        public List<Order> LoadOrdersFromFile(string date)
        {
            List<Order>OrderList = new List<Order>();

            if (File.Exists(FILENAME + date + FILEEXT))
            {
                using (StreamReader sr = File.OpenText(FILENAME + date + FILEEXT))
                {
                    string inputLine;

                    while ((inputLine = sr.ReadLine()) != null)
                    {
                        try
                        {
                            string[] inputParts = inputLine.Split('|');
                            Order thisOrder = new Order()
                            {
                                OrderNumber = int.Parse(inputParts[0]),
                                CustomerName = inputParts[1],
                                Area = decimal.Parse(inputParts[2]),
                                OrderProduct = prodRepo.GetProductBy(inputParts[3]),
                                OrderState = stateRepo.GetTaxBy(inputParts[4])
                            };

                            OrderList.Add(thisOrder);
                        }
                        catch
                        {
                            ErrorLog.Write($"Failed to read line Loading Order from File Orders_{date}.text");
                        }
                    }
                }
            }
            
            return OrderList;
        }

        public int GetNextOrderNumber()
        {
            string dateString = DateTime.Today.ToString("MMddyyyy");
            List<Order> orders = LoadOrdersFromFile(dateString);

            if (File.Exists(FILENAME + dateString + FILEEXT) == false || orders.Count == 0 )
            {
                return 1;
            }
            else
            {
                    int next = orders.First(x => x.OrderNumber == orders.Max(n => n.OrderNumber)).OrderNumber;
                    return ++next;
            }
        }

        public Order CreateOrder(Order order)
        {
            string dateString = DateTime.Today.ToString("MMddyyyy");

       
            List<Order> orders = LoadOrdersFromFile(dateString);
            order.OrderNumber = GetNextOrderNumber();
            orders.Add(order);

            try
            {
                using (StreamWriter sw = File.AppendText(FILENAME + dateString + FILEEXT))
                {
                    sw.WriteLine($"{order.OrderNumber}|{order.CustomerName}|{order.Area}|{order.OrderProduct.ProductType.ToUpper()}|{order.OrderState.StateAbbr.ToUpper()}");
                }
            }
            catch 
            {
                ErrorLog.Write($"Failed to write order {order.OrderNumber} for file Orders_{dateString}, client {order.CustomerName}");
            }

            return order;
        }

        public Order GetOrderByDateandNumber(string date, int number)
        {
            string dateString = "";
            
            dateString = DateTime.Parse(date).ToString("MMddyyyy");
            
            List<Order> OrdersForDate = LoadOrdersFromFile(dateString);

            return OrdersForDate.FirstOrDefault(o => o.OrderNumber == number);
        }

        public void RemoveOrder(Order order, string date)
        {
            string dateString = "";

            dateString = DateTime.Parse(date).ToString("MMddyyyy");
            List<Order> orders =  LoadOrdersFromFile(dateString);

            if (orders != null)
            {
                Order orderToRemove = orders.FirstOrDefault(x => x.OrderNumber == order.OrderNumber);

                orders.Remove(orderToRemove);

                using (StreamWriter sw = new StreamWriter(FILENAME + dateString + FILEEXT))
                {
                    try
                    {
                        foreach (var o in orders)
                        {
                            sw.WriteLine($"{o.OrderNumber}|{o.CustomerName}|{o.Area}|{o.OrderProduct.ProductType}|{o.OrderState.StateAbbr}");

                        }
                    }
                    catch 
                    {
                        ErrorLog.Write($"Failed to rewrite order list for {dateString} after order removed");
                    }
                }
            }

        }

        public void CancelOrder(Order order, string date)
        {
            List<Order> orders = LoadOrdersFromFile(date);

            if (orders != null)
            {
                Order orderToRemove = orders.FirstOrDefault(x => x.OrderNumber == order.OrderNumber);

                orders.Remove(orderToRemove);

                using (StreamWriter sw = new StreamWriter(FILENAME + date + FILEEXT))
                {
                    try
                    {
                        foreach (var o in orders)
                        {
                            sw.WriteLine($"{o.OrderNumber}|{o.CustomerName}|{o.Area}|{o.OrderProduct.ProductType}|{o.OrderState.StateAbbr}");
                        }
                    }
                    catch 
                    {
                        ErrorLog.Write($"Failed to rewrite order list for {date} after cancelled update");
                    }
                }
            }
        }

        public List<Order> LoadOrderList(string date)
        {
           return LoadOrdersFromFile(date);
        }

        public Order UpdatedOrder(Order order)
        {
            string dateString = "";

            dateString = DateTime.Parse(order.OrderDate).ToString("MMddyyyy");
            List<Order> orders = LoadOrdersFromFile(dateString);

            orders.Add(order);

            try
            {
                using (StreamWriter sw = File.AppendText(FILENAME + dateString + FILEEXT))
                {
                    sw.WriteLine($"{order.OrderNumber}|{order.CustomerName}|{order.Area}|{order.OrderProduct.ProductType.ToUpper()}|{order.OrderState.StateAbbr.ToUpper()}");
                }
            }
            catch
            {
                ErrorLog.Write($"Failed to rewrite file after updating order {order.OrderNumber} for Orders_{dateString}");
            }

            return order;
        }

    }
}
