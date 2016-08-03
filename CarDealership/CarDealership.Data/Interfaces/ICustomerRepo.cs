using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.Data.Interfaces
{
    public interface ICustomerRepo
    {
        
        List<Customer> GetAll();
        Customer GetCustomer(int id);
        Customer AddCustomer(Customer newCustomer);
        void EditCustomer(int id);
        void AddEmail(int custId, string email);
        void AddPhone(int custId, string phone, string name);
    }
}
