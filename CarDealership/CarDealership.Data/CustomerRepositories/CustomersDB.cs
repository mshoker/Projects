using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;

namespace CarDealership.Data.CustomerRepositories
{
    public class CustomersDB :ICustomerRepo
    {
        private static string _connectionString =
            ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString;
        
        public List<Customer> GetAll()
        {
           List<Customer> customers = new List<Customer>();

            //connect to DB
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                //sql query to get the data
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText =
                    "SELECT C.CustomerID, C.Name, P.Number, P.Name AS PhoneName, P.PhoneID, E.[Address], E.EmailID " +
                    "FROM Customers C " +
                    "LEFT OUTER JOIN Email E " +
                    "ON E.CustomerID = C.CustomerID " +
                    "LEFT OUTER JOIN PhoneNumber P " +
                    "ON P.CustomerID = C.CustomerID";

                cn.Open();

                //allocate data to customer object
                using (var dr = cmd.ExecuteReader())
                {
                    customers = PopulateCustomerListFromReader(dr);
                }
            }

            return customers;
        }

        private Customer PopulateCustomerFromReader(SqlDataReader dr)
        {
            Customer newCustomer = new Customer()
            {
                CustomerID = (int)dr["CustomerID"],
                Name = dr["Name"].ToString(),
                PhoneNumbers = new List<Phone>(),
                Emails = new List<Email>()
            };

            return newCustomer;
        }

        private Phone PopulatePhoneFromReader(SqlDataReader dr)
        {
            Phone newPhone = new Phone();
            newPhone.PhoneID = (int)dr["PhoneID"];
            newPhone.PhoneNumber = dr["Number"].ToString();
            newPhone.Name = dr["PhoneName"].ToString();

            return newPhone;
        }

        private Email PopulateEmailFromReader(SqlDataReader dr)
        {
            Email newEmail = new Email()
            {
                EmailID = int.Parse(dr["EmailID"].ToString()),
                EmailAddress = dr["Address"].ToString()
            };

            return newEmail;
        }

        private List<Customer> PopulateCustomerListFromReader(SqlDataReader dr)
        {
            List<Customer> customers = new List<Customer>();
            while (dr.Read())
            {
                Customer newCustomer = customers.FirstOrDefault(c => c.CustomerID == (int)dr["CustomerID"]);
                if (newCustomer == null)
                {
                    customers.Add(PopulateCustomerFromReader(dr));
                }

                //adding phone numbers to list
                if (customers.First(c => c.CustomerID == (int)dr["CustomerID"]).PhoneNumbers.Count == 0
                    || customers.First(c => c.CustomerID == (int)dr["CustomerID"]).PhoneNumbers.Exists(p => p.PhoneID == (int)dr["PhoneID"]) == false)
                {
                    customers.First(c => c.CustomerID == (int)dr["CustomerID"]).PhoneNumbers.Add(PopulatePhoneFromReader(dr));
                }

                //adding emails to list
                if (
                    customers.First(c => c.CustomerID == (int)dr["CustomerID"]).Emails.FirstOrDefault(
                        e => e.EmailID == (int)dr["EmailID"]) == null)
                {
                    customers.First(c => c.CustomerID == (int)dr["CustomerID"]).Emails.Add(PopulateEmailFromReader(dr));
                }
            }

            return customers;
        }
        
        public Customer GetCustomer(int id)
        {
            List<Customer> customers = new List<Customer>();

            using (
                var cn = new SqlConnection(ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText =
                        $"select c.CustomerID, c.Name, e.[Address], p.Number, p.Name  " +
                        $"from Customers c " +
                        $"left outer join email e " +
                        $"on c.CustomerID = e.CustomerID " +
                        $"left outer join PhoneNumber p " +
                        $"on p.CustomerID = c.CustomerID " +
                        $"where c.CustomerID = {id}";

                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (customers.Count == 0)
                                {
                                    customers.Add(PopulateCustomerFromReader(dr));
                                }

                                //get phone numbers
                                if(customers.First().PhoneNumbers.Exists(p => p.PhoneID == (int)dr["PhoneID"]) == false)
                                {
                                    customers.First().PhoneNumbers.Add(PopulatePhoneFromReader(dr));
                                }

                                //get emails
                                if (customers.First().Emails.FirstOrDefault(
                                e => e.EmailID == (int)dr["EmailID"]) == null)
                                {
                                    customers.First().Emails.Add(PopulateEmailFromReader(dr));
                                }
                            }
                        }
                    }
                }
            }

            return customers.FirstOrDefault();
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            //add customer
            using (
                var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = $"insert into Customers (Name) values ('{newCustomer.Name}')",
                    Connection = cn
                };

                List<int> customerIDs = new List<int>();

                SqlCommand cmdList = new SqlCommand()
                {
                    CommandText = "select CustomerID from Customers " +
                                  $"where Name = '{newCustomer.Name}' " +
                                  "order by CustomerID DESC ",
                    Connection = cn
                };



                cn.Open();
                cmd.ExecuteNonQuery();
                using (var dr = cmdList.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        customerIDs.Add(int.Parse(dr["CustomerID"].ToString()));
                    }
                }
                newCustomer.CustomerID = customerIDs.First();

                //get new id

            }

            
            newCustomer.PhoneNumbers = new List<Phone>();
            return newCustomer;
        }

        public void EditCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void AddEmail(int custId, string email)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = $"INSERT INTO Email (CustomerID, [Address]) VALUES ('{custId}', '{email}')",
                    Connection = cn
                };

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddPhone(int custId, string phone, string name)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = $"INSERT INTO PhoneNumber (CustomerID, Number, Name) VALUES ('{custId}', '{phone}', '{name}')",
                    Connection = cn
                };

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
