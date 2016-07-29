using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data.Interfaces;
using FlooringMastery.Data.OrderRepositories;
using FlooringMastery.Data.ProductRepositories;
using FlooringMastery.Data.TaxRepositories;
using FlooringMastery.Models;

namespace FlooringMastery.BLL
{
    public class OrderFunctions
    {
        private IOrderRepository _orderRepo { get; set; }
        private IProductRepository _prodRepo { get; set; }
        private ITaxRepository _stateSrepo { get; set; }

        public OrderFunctions(IOrderRepository oRepo, IProductRepository pRepo, ITaxRepository srepo)
        {
            _orderRepo = oRepo;
            _prodRepo = pRepo;
            _stateSrepo = srepo;
        }
        public Response GetOrder(string orderDate, int orderNumber)
        { 
            Response response = new Response();

            Order queryOrder = _orderRepo.GetOrderByDateandNumber(orderDate, orderNumber);

            if (queryOrder != null)
            {
                response.IsSuccessful = true;
                response.OrderInfo = queryOrder;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "This is not the order you are looking for";
            }

            return response;
        }

        public Response AddOrder(string name, string product, string stateAbbr, decimal area)
        {
            
            var response = new Response();
            int orderNumber = _getNextOrderNumber();
            string date = DateTime.Today.ToShortDateString();
            string dateString = DateTime.Parse(date).ToString("MMddyyyy");

            Order newOrder = new Order()
            {
                CustomerName = name,
                Area = area,
                OrderDate = dateString,
                OrderProduct = _prodRepo.GetProductBy(product),
                OrderState = _stateSrepo.GetTaxBy(stateAbbr),
                OrderNumber = orderNumber
            };

            newOrder = _orderRepo.CreateOrder(newOrder);

            if (newOrder != null)
            {
                response.IsSuccessful = true;
                response.OrderInfo = newOrder;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"Failed to create order";
            }

            return response;

        }

        public Response EditOrder(Order orderToBeUpdated, string name, string state, string product, decimal area)
        {
            var response = new Response();
            Order updatedOrder = orderToBeUpdated;
            if (name == "" && state == "" && product == "" && area == 0)
            {
                response.IsSuccessful = false;
                response.OrderInfo = orderToBeUpdated;
                return response;
            }
            else
            {
                switch (name)
                {
                    case null:
                    case "":
                        break;
                    default:
                        updatedOrder.CustomerName = name;
                        break;
                }
                switch (state)
                {
                    case null:
                    case "":
                        break;
                    default:
                        updatedOrder.OrderState = _stateSrepo.GetTaxBy(state);
                        break;
                }
                switch (product)
                {
                    case null:
                    case "":
                        break;
                    default:
                        updatedOrder.OrderProduct = _prodRepo.GetProductBy(product);
                        break;
                }

                if (area > 0)
                {
                    updatedOrder.Area = area;
                }

                response.OrderInfo = updatedOrder;
                response.IsSuccessful = true;
            }
            
           

            return response;
        }

        public void AddUpdatedOrder(string date, Order order)
        {
            _orderRepo.UpdatedOrder(order);
        }

        public void RemoveOrder(Order order, string date)
        {
            _orderRepo.RemoveOrder(order,  date);
        }

        public decimal TotalMaterialCosts(Order order)
        {
            decimal TotalMaterialCosts = order.Area * order.OrderProduct.CostPerSquFoot;

            return TotalMaterialCosts;
        }

        public decimal TotalLabourCosts(Order order)
        {
            decimal TotalLabourCosts = order.Area*order.OrderProduct.LabourPerSquFoot;
            return TotalLabourCosts;
        }

        public decimal TotalCosts(Order order)
        {
            decimal TotalCosts = TotalMaterialCosts(order) + TotalLabourCosts(order);
            return TotalCosts;
        }

        private int _getNextOrderNumber()
        {
            return _orderRepo.GetNextOrderNumber();
        }

        public bool ValidateProductInput(string productName)
        {
            if (_prodRepo.GetProductBy(productName.ToUpper()) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateStateInput(string stateAbbr)
        {
            if (_stateSrepo.GetTaxBy(stateAbbr.ToUpper()) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CancelOrder(Order order, string date)
        {
            _orderRepo.CancelOrder(order, date);
        }

        public List<Order>ListAllOrders(string date)
        {
            return _orderRepo.LoadOrderList(date);
        }

        public List<string> ListAllProduts()
        {
           return _prodRepo.ListAllProductNames();
        }

        public List<string> ListAllStates()
        {
            return _stateSrepo.ListAllStates();
        }

    }
}
