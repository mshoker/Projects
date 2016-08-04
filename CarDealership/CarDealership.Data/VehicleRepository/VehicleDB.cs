using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;
using CarDealership.Models.Enums;
using Dapper;

namespace CarDealership.Data.VehicleRepository
{
    public class VehicleDB : IVehicleRepo
    {
        public List<Vehicle> GetAll()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Vehicle";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vehicles.Add(PopulateVehicleFromReader(dr));
                        }
                    }
                }
            }

            return vehicles;
        }

        public List<Model> GetAllModels()
        {
            List<Model> models = new List<Model>();
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Model";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            models.Add(PopulateModelFromReader(dr));
                        }
                    }
                }
            }

            return models;
        }

        public List<Make> GetAllMakes()
        {
            List<Make> makes = new List<Make>();
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Makes";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            makes.Add(PopulateMakeFromReader(dr));
                        }
                    }
                }
            }

            return makes;
        }

        private Vehicle PopulateVehicleFromReader(SqlDataReader dr)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.VehicleID = (int) dr["VehicleID"];
            vehicle.SellerID = (int)dr["SellerID"];
            vehicle.Year = DateTime.Parse(dr["Year"].ToString());
            vehicle.AdTitle = dr["AdTitle"].ToString();
            vehicle.Description = dr["Description"].ToString();
            vehicle.Price = (decimal) dr["Price"];
            vehicle.ImgUrl = dr["ImgUrl"].ToString();
            vehicle.Condition = (CarCondition) (int) dr["Condition"];
            vehicle.Model = GetModel(int.Parse(dr["ModelId"].ToString()));

            return vehicle;
        }

        private Model PopulateModelFromReader(SqlDataReader dr)
        {
            Model model = new Model()
            {
                ModelID = int.Parse(dr["ModelId"].ToString()),
                Make = GetMake(int.Parse(dr["MakeId"].ToString())),
                Description = dr["Description"].ToString(),
                Name = dr["Name"].ToString()
            };

            return model;
        }

        private Make PopulateMakeFromReader(SqlDataReader dr)
        {
            Make make = new Make()
            {
                MakeID = int.Parse(dr["MakeID"].ToString()),
                Name = dr["Brand"].ToString()
            };


            return make;
        }

        private Model GetModel(int ModelID)
        {
            Model model = new Model();
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Model WHERE Model.ModelID = {ModelID}";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                model = PopulateModelFromReader(dr);
                            }
                        }

                    }
                }
            }

            return model;
        }

        private Make GetMake(int MakeID)
        {
            Make make = new Make();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Makes WHERE Makes.MakeID = {MakeID}";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                make = PopulateMakeFromReader(dr);
                            }
                        }
                    }
                }
            }

            return make;
        }

        public Vehicle GetById(int id)
        {
            Vehicle requested = new Vehicle();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Vehicle WHERE VehicleID = {id}";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                return PopulateVehicleFromReader(dr);
                            }
                        }
                    }
                }
            }

            return requested;
        }

        public List<Vehicle> GetByModel(int id)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Vehicle WHERE ModelID = {id}";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vehicles.Add(PopulateVehicleFromReader(dr));
                        }
                    }
                }
            }

            return vehicles;
        }

        public List<Vehicle> GetByMake(int id)
        {

            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT v.AdTitle, v.Condition, v.[Description], v.ImgUrl, v.ModelID, v.Price, v.SellerID, v.VehicleID, v.[Year] " +
                                      $"FROM Vehicle v " +
                                      $"inner join Model m " +
                                      $"on v.ModelID = m.ModelID " +
                                      $"WHERE MakeID = {id}";
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vehicles.Add(PopulateVehicleFromReader(dr));
                        }
                    }
                }
            }

            return vehicles;
        }

        public List<Vehicle> GetBySeller(int id)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Vehicle WHERE SellerID = @SellerID";
                    cmd.Parameters.AddWithValue("@SellerID", id);

                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vehicles.Add(PopulateVehicleFromReader(dr));
                        }
                    }
                }
            }

            return vehicles;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText =
                    $"INSERT INTO Vehicle (SellerID, [Year], AdTitle, [Description], Price, ImgUrl, Condition, ModelID) " +
                    $"VALUES(@SellerID, @Year, @AdTitle , @Description, @Price, @ImgUrl, @Condition, @ModelID)";
                cmd.Parameters.AddWithValue("@SellerID", vehicle.SellerID);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@AdTitle", vehicle.AdTitle);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@Price", vehicle.Price);
                cmd.Parameters.AddWithValue("@ImgUrl", vehicle.ImgUrl);
                cmd.Parameters.AddWithValue("@Condition", (int)vehicle.Condition);
                cmd.Parameters.AddWithValue("@ModelID", vehicle.Model.ModelID);

                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddModel(Model model)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Model(MakeID, Name, [Description]) VALUES (@MakeID, @Name, @Description)";
                    cmd.Parameters.AddWithValue("@MakeID", model.Make.MakeID);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cn.Open();
                }
            }
        }

        public void AddMake(Make make)
        {
            
        }

        public void RemoveVehicle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
