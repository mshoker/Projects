using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;
using CarDealership.Models.Enums;

namespace CarDealership.Data.VehicleRepository
{
    public class VehiclesMOCK: IVehicleRepo
    {
        private static List<Vehicle> _vehicles;
        private static List<Make> _makes;
        private static List<Model> _models;

        public VehiclesMOCK()
        {
            if (_makes == null)
            {
                _makes = new List<Make>()
                {
                    new Make()
                    {
                        MakeID = 1,
                        Name = "Ford"
                    },
                    new Make()
                    {
                        MakeID = 2,
                        Name = "Holden"
                    },
                    new Make()
                    {
                        MakeID = 3,
                        Name = "Toyota"
                    },
                    new Make()
                    {
                        MakeID = 4,
                        Name = "Nissan"
                    }
                };
            }

            if (_models == null)
            {
                _models = new List<Model>()
                {
                    new Model()
                    {
                        Make = GetMake(1),
                        ModelID = 1,
                        Name = "F-150",
                        Description = "Our latest generation of the Ford F-150 is lighter, " +
                                      "stronger, more powerful and more capable. It’s built on the principle " +
                                      "that the best truck for today is the one engineered to meet the challenges " +
                                      "of performance, efficiency and dependability long into the future."
                    },
                    new Model()
                    {
                        Make = GetMake(2),
                        ModelID = 2,
                        Name = "Commodore",
                        Description = "210kW 3.6 litre SIDI Direct Injection V6 engine + Head Up Display + " +
                                      "Satellite Navigation"
                    },
                    new Model()
                    {
                        Make = GetMake(3),
                        ModelID = 3,
                        Name = "RAV4",
                        Description = "Get Fun and Reliability in One SUV"
                    },
                    new Model()
                    {
                        Make = GetMake(4),
                        ModelID = 4,
                        Name = "Titan",
                        Description = "TITAN offers more of what you’re looking for, " +
                                      "from its powerful Endurance® V8 to its fully boxed, " +
                                      "full-length ladder frame. For work, play, and everything in between - " +
                                      "TITAN is ready"
                    }
                };
            }

            if (_vehicles == null)
            {
                _vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        VehicleID = 1,
                        Model = GetModel(1),
                        Condition = CarCondition.LightlyUsed,
                        IsAvailable = true,
                        AdTitle = "Big Truck",
                        Description = "Big Red Car",
                        ImgUrl = "~/Content/app/images/63715731.jpg",
                        Year = DateTime.Parse("7/6/2014"),
                        Price = 25500
                    },
                    new Vehicle()
                    {
                        VehicleID = 2,
                        Model = GetModel(2),
                        Condition = CarCondition.VeryUsed,
                        IsAvailable = true,
                        AdTitle = "Classic Commodore",
                        Description = "Little Silver Car",
                        ImgUrl = "~/Content/app/images/2commodore.jpg",
                        Year = DateTime.Parse("7/6/2000"),
                        Price = 600
                    }
                };
            }
        }



        public List<Vehicle> GetAll()
        {
            return _vehicles;
        }

        public Vehicle GetById(int id)
        {
            return _vehicles.FirstOrDefault(v => v.VehicleID == id);
        }
        
        public List<Vehicle> GetByModel(int id)
        {
            return _vehicles.Where(m => m.Model.ModelID == id).ToList();
        }

        public List<Vehicle> GetByMake(int id)
        {
            return _vehicles.Where(m => m.Model.Make.MakeID == id).ToList();
        }

        private int GetNextVehicleId()
        {
            return (_vehicles.Max(m => m.VehicleID) + 1);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicle.VehicleID = GetNextVehicleId();
            _vehicles.Add(vehicle);
        }

        private int GetNextModelId()
        {
            return (_models.Max(m => m.ModelID) + 1);
        }

        public void AddModel(Model model)
        {
            model.ModelID = GetNextModelId();
            _models.Add(model);
        }

        private int GetNextMakeId()
        {
            return (_makes.Max(m => m.MakeID) + 1);
        }

        public void AddMake(Make make)
        {
            make.MakeID = GetNextMakeId();
            _makes.Add(make);
        }

        public void RemoveVehicle(int id)
        {
            Vehicle vehicle = GetById(id);

            _vehicles.Remove(vehicle);
        }

        public List<Vehicle> GetBySeller(int id)
        {
            return _vehicles.Where(s => s.SellerID == id).ToList();
        }

        public Make GetMake(int id)
        {
            return _makes.FirstOrDefault(m => m.MakeID == id);
        }

        public Model GetModel(int id)
        {
            return _models.FirstOrDefault(m => m.ModelID == id);
        }
    }
}
