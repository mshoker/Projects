using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;

namespace CarDealership.Data.CustomerRepositories
{
    public class CustomersMOCK: ICustomerRepo
    {
        private static List<Customer> _customers;

        public CustomersMOCK()
        {
            if (_customers == null)
            {

                _customers = new List<Customer>()
                {
                    new Customer()
                    {
                        CustomerID = 1,
                        Name = "Bob Evans",
                        Emails = new List<Email>()
                        {
                            new Email()
                            {
                                EmailID = 1,
                                EmailAddress = "bevans@bobevans.com"
                            },
                            new Email()
                            {
                                EmailID = 2,
                                EmailAddress = "bobby@bestbananabread.com"
                            }
                        },
                        PhoneNumbers = new List<Phone>()
                        {
                            new Phone()
                            {
                                PhoneID = 1,
                                PhoneNumber = "513-878-6789"
                            },

                            new Phone()
                            {
                                PhoneID = 2,
                                PhoneNumber = "513-765-4321"
                            }
                        }
                    },

                    new Customer()
                    {
                        CustomerID = 2,
                        Name = "Ronald McDonald",
                        Emails = new List<Email>()
                        {
                            new Email()
                            {
                                EmailID = 3,
                                EmailAddress = "rmcdonald@mcdonalds.com"
                            },
                            new Email()
                            {
                                EmailID = 4,
                                EmailAddress = "ronny@burgers.com"
                            }
                        },
                        PhoneNumbers = new List<Phone>()
                        {
                            new Phone()
                            {
                                PhoneID = 3,
                                PhoneNumber = "513-123-4567"
                            }
                        }
                    }

                };
            }
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetCustomer(int id)
        {
            return _customers.FirstOrDefault(c => c.CustomerID == id);
        }

        public int GetNextCustomerId()
        {
            return (_customers.Max(c => c.CustomerID) + 1);
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            newCustomer.CustomerID = GetNextCustomerId();
            newCustomer.Emails = new List<Email>();
            newCustomer.PhoneNumbers = new List<Phone>();
            _customers.Add(newCustomer);

            return newCustomer;
        }

        public void EditCustomer(int id)
        {
            throw new NotImplementedException();
        }

        private int GetNextEmailId(int customerId)
        {
            var emails = GetCustomer(customerId).Emails;
            if (emails.Count == 0)
            {
                return 1;
            }
            else
            {
                return (emails.Max(m => m.EmailID) + 1);
            }
           }

        public void AddEmail(int custId, string email)
        {
            //change either cust list or ordering of phone numbers
            Email newEmail = new Email();
            newEmail.EmailID = GetNextEmailId(custId);
            newEmail.EmailAddress = email;

            GetCustomer(custId).Emails.Add(newEmail);

        }

        private int GetNextPhoneId(int customerId)
        {
            var phones = GetCustomer(customerId).PhoneNumbers;
            if (phones.Count == 0)
            {
                return 1;
            }
            else
            {
                return (phones.Max(m => m.PhoneID) + 1);
            }
        }

        public void AddPhone(int custId, string phone, string name)
        {
            Phone newPhone = new Phone();
            newPhone.PhoneID = GetNextPhoneId(custId);
            newPhone.PhoneNumber = phone;

            GetCustomer(custId).PhoneNumbers.Add(newPhone);
        }
    }
}
