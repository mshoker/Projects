using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.Data.Interfaces
{
    public interface IVehicleRepo
    {
        //get all vehicles
        List<Vehicle> GetAll();
        //get all makes
        List<Make> GetAllMakes();
        //get all models
        List<Model> GetAllModels();
        //get vehicle by id
        Vehicle GetById(int id);
        //get vehicles by model
        List<Vehicle> GetByModel(int id);
        //get vehicles by make
        List<Vehicle> GetByMake(int id);
        //add vehicle
        void AddVehicle(Vehicle vehicle);
        //add model
        void AddModel(Model model);
        //add make
        void AddMake(Make make);
        //remove vehicle
        void RemoveVehicle(int id);
        //get by seller
        List<Vehicle> GetBySeller(int id);
    }
}
