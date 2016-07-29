using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;
using HRPortal.Models;

namespace HRPortal.Data.Repositories.Employee
{
    public class EmployeeInMemory: IEmployeeRepository
    {
        private static List<Models.Employee> _employees;

        static EmployeeInMemory()
        {
            var cRepo = RepositoryFactory.Company();
            var jRepo = RepositoryFactory.Job();
            _employees = new List<Models.Employee>()
            {
                new Models.Employee()
                {
                    Id = 1,
                    Employer = cRepo.Get(2),
                    HireDate = DateTime.Parse("5/4/2010"),
                    FirstName = "Alastor",
                    LastName = "Moody",
                    Job = jRepo.Get(2),
                    WorkHistory = new List<Shift>()
                    {
                        new Shift()
                        {
                            Date = DateTime.Parse("5/4/2010"),
                            HoursWorked = 10,
                            Day = "5/4/2010",
                            StartTime = "9.00am"
                        },
                        new Shift()
                        {
                            Date = DateTime.Parse("5/10/2010"),
                            HoursWorked = 10,
                            Day = "5/4/2010",
                            StartTime = "9.00am"
                        }
                    }
                },
                new Models.Employee()
                {
                    Id = 2,
                    Employer = cRepo.Get(1),
                    HireDate = DateTime.Parse("5/4/2012"),
                    FirstName = "Don",
                    LastName = "Draper",
                    Job = jRepo.Get(1),
                    WorkHistory = new List<Shift>()
                    {
                        new Shift()
                        {
                            Date = DateTime.Parse("5/7/2010"),
                            HoursWorked = 10,
                            Day = "5/4/2010",
                            StartTime = "9.00am"
                        },
                        new Shift()
                        {
                            Date = DateTime.Parse("5/10/2010"),
                            HoursWorked = 10,
                            Day = "5/4/2010",
                            StartTime = "9.00am"
                        }
                    }
                }
            };
        }

        public IEnumerable<Models.Employee> GetAll()
        {
            return _employees;
        }

        public IEnumerable<Models.Employee> GetCompany(int compId)
        {
            return _employees.Where(e => e.Employer.CompanyId == compId).Distinct();
        }

        public Models.Employee Get(int id)
        {
            return _employees.First(e => e.Id == id);
        }

        private int GetNextId()
        {
            return (_employees.Select(e => e.Id).Max() + 1);
        }

        public void AddEmployee(Models.Employee employee)
        {
            employee.Id = GetNextId();
            employee.HireDate = DateTime.Today;
            employee.WorkHistory = new List<Shift>();

            _employees.Add(employee);
        }

        public void EditEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
