using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;
using HRPortal.Data.Repositories.Company;
using HRPortal.Data.Repositories.Employee;
using HRPortal.Data.Repositories.Job;
using HRPortal.Data.Repositories.Policy;

namespace HRPortal.Data.Repositories
{
    public class RepositoryFactory
    {
        
        public static ICompanyRepository Company()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "TEST":
                    return new CompanyInMemory();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static IJobRepository Job()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode.ToUpper())
            {
                case "TEST":
                    return new JobInMemory();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static IPolicyRepository Policy()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();


            switch (mode.ToUpper())
            {
                case "TEST":
                    return new PolicyInMemory();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }

        public static IEmployeeRepository Employee()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();


            switch (mode.ToUpper())
            {
                case "TEST":
                    return new EmployeeInMemory();
                case "PROD":
                    throw new NotImplementedException();
                default:
                    throw new Exception("I don't know that mode");
            }
        }
    }
}
