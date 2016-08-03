using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;
using CarDealership.Models.Enums;

namespace CarDealership.Data.Interfaces
{
    public interface IRequestRepo
    {
        List<Request> GetAll();
        Request GetBy(int id);
        List<Request> GetForCustomer(int customerID);
        List<Request> GetForVehicle(int vehicleID);
        void Add(Request request);
        void UpdateStatus(int requestID, RequestStatus status);

    }
}
