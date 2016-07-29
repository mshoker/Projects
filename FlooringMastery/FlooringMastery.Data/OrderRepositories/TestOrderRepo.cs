using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.Data.OrderRepositories
{
    public class TestOrderRepo: IOrderRepository
    {
        ITaxRepository taxRepo = TaxRepositoryFactory.Create();
        IProductRepository prodRepo = ProductRepositoryFactory.Create();

        public static List<Order> OrderList;

        public TestOrderRepo()
        {
            if (OrderList == null)
            {
                OrderList = new List<Order>()
                {
                    new Order()
                    {
                        CustomerName = "Charlie Brown",
                        Area = 3.00m,
                        OrderDate = DateTime.Parse("06/28/2016").ToShortDateString(),
                        OrderNumber = 1,
                        OrderProduct = prodRepo.GetProductBy("TILE"),
                        OrderState = taxRepo.GetTaxBy("OH")
                    },

                    new Order()
                    {
                        CustomerName = "Bob the Builder",
                        Area = 8.00m,
                        OrderDate = DateTime.Parse("06/28/2016").ToShortDateString(),
                        OrderNumber = 2,
                        OrderProduct = prodRepo.GetProductBy("TILE"),
                        OrderState = taxRepo.GetTaxBy("OH")
                    }
                };
            }
        }

        public int GetNextOrderNumber()
        {
            return 1 + OrderList.Select(x => x.OrderNumber).Max();
        }

        public Order CreateOrder(Order order)
        {
            OrderList.Add(order);

            return order;
        }

        public  void RemoveOrder(Order order, string date)
        {
            OrderList.Remove(order);
            
        }

        public Order GetOrderByDateandNumber(string date, int number)
        {
            return OrderList.Where(d => d.OrderDate == date).FirstOrDefault(x => x.OrderNumber == number);
        }

        public Order UpdatedOrder(Order order)
        {
            OrderList.Add(order);

            return order;
        }

        public void CancelOrder(Order order, string date)
        {
            OrderList.Remove(order);
        }

        public List<Order> LoadOrderList(string date)
        {
            string dateFormatted = DateTime.Parse(date).ToShortDateString();
            return OrderList.Where(d => d.OrderDate == dateFormatted).ToList();
        }
    }
}
