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
    public class RequestsMOCK: IRequestRepo
    {
        private List<Request> _requests;
        private IVehicleRepo vRepo = RepositoryFactory.Vehicle();
        private ICustomerRepo cRepo = RepositoryFactory.Customer();

        public RequestsMOCK()
        {
            if (_requests == null)
            {
                _requests = new List<Request>()
                {
                    new Request()
                    {
                        RequestID = 1,
                        Vehicle = vRepo.GetById(1),
                        Customer = cRepo.GetCustomer(1),
                        BestContactMethod = ContactMethods.Email,
                        PreferredDays = DaysOfTheWeek.Monday,
                        BestTimeToCall = TimeOfDay.Morning,
                        Status = RequestStatus.New

                    }
                };
            }
        }

        public List<Request> GetAll()
        {
            return _requests;
        }

        public Request GetBy(int id)
        {
            return _requests.FirstOrDefault(r => r.RequestID == id);
        }

        public List<Request> GetForCustomer(int customerID)
        {
            return _requests.Where(r => r.Customer.CustomerID == customerID).ToList();
        }

        public List<Request> GetForVehicle(int vehicleID)
        {
            return _requests.Where(r => r.Vehicle.VehicleID == vehicleID).ToList();
        }

        private int GetNextId()
        {
            return (_requests.Max(r => r.RequestID) + 1);
        }

        public void Add(Request request)
        {
            request.RequestID = GetNextId();
            _requests.Add(request);
        }

        public void UpdateStatus(int requestID, RequestStatus status)
        {
            Request request = GetBy(requestID);
            request.Status = status;
        }
    }
}
