using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.CustomerRepositories;
using CarDealership.Data.Interfaces;
using CarDealership.Models;

namespace CarDealership.Data.SellerRepositories
{
    public class SellerDB: ISellerRepo
    {
        private static string _connectionString =
            ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString;

        private IVehicleRepo vRepo = RepositoryFactory.Vehicle();

        public List<Seller> ListAll()
        {
            List<Seller> sellers = new List<Seller>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Seller S " +
                                  "LEFT OUTER JOIN Vehicle V " +
                                  "ON S.SellerID = V.SellerID";

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    sellers = PopulateSellerListFromReader(dr);
                }
            }

            return sellers;
        }

        private List<Seller> PopulateSellerListFromReader(SqlDataReader dr)
        {
            List<Seller> sellers = new List<Seller>();
            while (dr.Read())
            {
                Seller newSeller = sellers.FirstOrDefault(s => s.SellerID == (int) dr["SellerID"]);
                if (newSeller == null)
                {
                    sellers.Add(PopulateSellerFromReader(dr));
                }
                

            }
            return sellers;
        }

        private Seller PopulateSellerFromReader(SqlDataReader dr)
        {
            Seller seller = new Seller()
            {
                SellerID = (int)dr["SellerID"],
                Name = dr["Name"].ToString(),
                Requests = new List<Request>(),
                Vehicles = vRepo.GetBySeller((int)dr["SellerID"])
            };

            //add vehicles
            
            //add requests

            return seller;
        }

        public Seller GetBy(int id)
        {
            Seller seller = new Seller();
            using (var cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Seller WHERE SellerID = @SellerID";
                cmd.Parameters.AddWithValue("@SellerID", id);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            seller = PopulateSellerFromReader(dr);
                        }
                    }
                }
            }

            return seller;
        }

        public Seller Add(Seller seller)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "INSERT INTO Seller (Name) VALUES (@Name)",
                    Connection = cn
                };
                cmd.Parameters.AddWithValue("@Name", seller.Name);

                List<int> SellerIDs = new List<int>();

                SqlCommand cmdID = new SqlCommand()
                {
                    CommandText = "SELECT SellerID FROM Seller WHERE Name = @Name ORDER BY SellerID DESC",
                    Connection = cn
                };

                cmdID.Parameters.AddWithValue("@Name", seller.Name);
                
                cn.Open();
                cmd.ExecuteNonQuery();

                using (var dr = cmdID.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SellerIDs.Add(int.Parse(dr["SellerID"].ToString()));
                    }
                }

                seller.SellerID = SellerIDs.First();
                seller.Vehicles = new List<Vehicle>();
                seller.Requests = new List<Request>();
                return seller;
            }
        }

        public Seller Edit(Seller seller)
        {
            throw new NotImplementedException();
        }
    }
}
