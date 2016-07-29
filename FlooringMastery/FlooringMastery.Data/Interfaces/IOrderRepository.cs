using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data.Interfaces
{
    public interface IOrderRepository
    {
        int GetNextOrderNumber();

        Order CreateOrder(Order order);

        void RemoveOrder(Order order, string date);

        Order GetOrderByDateandNumber(string date, int number);

        Order UpdatedOrder(Order order);

        void CancelOrder(Order order, string date);

        List<Order> LoadOrderList(string date);
    }
}
