using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Interfaces;
using CarDealership.Models;
using CarDealership.Models.Enums;

namespace CarDealership.Data.RequestRepositories
{
    public class RequestsDB: IRequestRepo
    {
        public List<Request> GetAll()
        {
            throw new NotImplementedException();
        }

        public Request GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public List<Request> GetForCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        public List<Request> GetForVehicle(int vehicleID)
        {
            throw new NotImplementedException();
        }

        public void Add(Request request)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int requestID, RequestStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
