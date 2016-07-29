using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models;
using HRPortal.Models.Enums;

namespace HRPortal.Data.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company Get(int id);
        IEnumerable<Job> GetOpenings(Company company);
        void AddApplication(int companyId, Application application);
        void EditStatus(int companyId, int applicationId, ApplicationStatus status);
        void DeleteOpening(int companyId, Models.Job job);

    }
}
