using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;

namespace CarDealership.BLL
{
    public class Operations
    {
        private readonly IVehicleRepo _vRepo;
        private readonly ICustomerRepo _cRepo;
        private readonly IRequestRepo _rRepo;
        private readonly ISellerRepo _sRepo;

        public Operations(IVehicleRepo cars, ICustomerRepo customers, IRequestRepo requests, ISellerRepo sellers)
        {
            _vRepo = cars;
            _cRepo = customers;
            _rRepo = requests;
            _sRepo = sellers;
        }
        
        public List<Vehicle> AllCars()
        {
            return _vRepo.GetAll();
        }

        public Vehicle GetVehicle(int id)
        {
            return _vRepo.GetById(id);
        }

        public List<Customer> GetAll()
        {
            return _cRepo.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return _cRepo.GetCustomer(id);
        }

        public void AddNewRequest(Request request)
        {
            _rRepo.Add(request);
        }

        public Customer AddCustomer(Customer customer)
        {
            return _cRepo.AddCustomer(customer);
        }

        public void AddEmail(int custId, string email)
        {
            _cRepo.AddEmail(custId, email);
        }

        public void AddPhone(int custId, string phone, string name)
        {
            _cRepo.AddPhone(custId, phone, name);
        }

        public Seller AddSeller(Seller seller)
        {
            return _sRepo.Add(seller);
        }

        public Seller GetSeller(int id)
        {
            return _sRepo.GetBy(id);
        }

        public List<Make> GetAllMakes()
        {
            return _vRepo.GetAllMakes();
        }

        public List<Model> GetAllModels()
        {
            return _vRepo.GetAllModels();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _vRepo.AddVehicle(vehicle);
        }
    }
}
