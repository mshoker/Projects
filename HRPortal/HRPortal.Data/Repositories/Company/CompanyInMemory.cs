using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;
using HRPortal.Data.Repositories.Job;
using HRPortal.Models;
using HRPortal.Models.Enums;

namespace HRPortal.Data.Repositories.Company
{
    public class CompanyInMemory : ICompanyRepository
    {
            private static List<Models.Company> _companies;
        
            static CompanyInMemory()
            {
                var JobRepo = RepositoryFactory.Job();
                //sample data
                _companies = new List<Models.Company>()
            {
                new Models.Company() {CompanyId = 1, Name = "ABC",
                    Openings = new List<Models.Job>() {JobRepo.Get(1) },
                    Applications = new List<Application>()
                    {
                        new Application() {ApplicationId = 1, FirstName = "Bob", LastName = "Barker", Resume = "I am really good",
                            Status = ApplicationStatus.Interviewing, Job = JobRepo.Get(1) }
                    } },
                new Models.Company() {CompanyId = 2, Name = "DEF",
                    Openings = new List<Models.Job>() {JobRepo.Get(2) },
                    Applications = new List<Application>()
                    {
                        new Application() {ApplicationId = 2, FirstName = "Harry", LastName = "Potter", Resume = "Thwarted Voldemort",
                            Status = ApplicationStatus.Interviewing, Job = JobRepo.Get(2)}
                    }},
                new Models.Company() {CompanyId = 3, Name = "GHI",
                    Openings = new List<Models.Job>() {JobRepo.Get(3) },
                    Applications = new List<Application>()
                    {
                        new Application() {ApplicationId = 3, FirstName = "Jack", LastName = "Sparrow", Resume = "Captain of Black Pearl",
                            Status = ApplicationStatus.NeedsToBeReviewed, Job = JobRepo.Get(3)}
                    }}
            };
            }

        public IEnumerable<Models.Company> GetAll()
        {
            return _companies;
        }

        public Models.Company Get(int id)
        {
            return _companies.FirstOrDefault(c => c.CompanyId == id);
        }

        public IEnumerable<Models.Job> GetOpenings(Models.Company company)
        {
            return company.Openings;
        }

        public int GetNextApplicationId(int companyId)
        {
            var applications = Get(companyId).Applications;
            return (applications.Select(i => i.ApplicationId).Max() + 1);
        }

        public void AddApplication(int companyId, Application application)
        {
            var applications = Get(companyId).Applications;
            application.ApplicationId = GetNextApplicationId(companyId);
            applications.Add(application);
        }

        public void EditStatus(int companyId, int applicationId, ApplicationStatus status)
        {
            var applications = Get(companyId).Applications;
            var selectedApplication = applications.First(a => a.ApplicationId == applicationId);
            selectedApplication.Status = status;
        }

        public void DeleteOpening(int companyId, Models.Job job)
        {
            Get(companyId).Openings.Remove(job);
            return;
        }
    }
}
