using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;
using HRPortal.Models;
using HRPortal.Models.Enums;

namespace HRPortal.BLL
{
    public  class Operations
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public Operations(IPolicyRepository policyRepository, ICompanyRepository companyRepository, IJobRepository jobRepository, IEmployeeRepository employeeRepository)
        {
            _policyRepository = policyRepository;
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _employeeRepository = employeeRepository;
        }

        public Company GetCompany(int companyId)
        {
            return _companyRepository.Get(companyId);
        }

        public Job GetJob(int jobId)
        {
            return _jobRepository.Get(jobId);
        }

        public void AddApplication(int companyId, Application application)
        {
            _companyRepository.AddApplication(companyId, application);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAll();
        }

        public IEnumerable<Job> GetAllJobs()
        {
           return _jobRepository.GetAll();
        }
        public IEnumerable<Job> GetAllJobOpenings()
        {
            var companies = _companyRepository.GetAll();
            List<Job> openings = new List<Job>();

            foreach (var company in companies)
            {
                foreach (var job in company.Openings)
                {
                    openings.Add(job);
                }
            }

            return openings.Distinct();
        }

        public IEnumerable<Company> GetOpenings(int id)
        {
            var companies = _companyRepository.GetAll();
            var openings = new List<Company>();

            foreach (var company in companies)
            {
                for (int i = 0; i < company.Openings.Count; i++)
                {
                    if (company.Openings[i].JobId == id)
                    {
                        openings.Add(company);
                    }
                }
            }

            return openings.Distinct();
        }

        public List<string> ListCategories()
        {
            return _policyRepository.GetCategories();
        }

        public List<Policy> ListPolicies()
        {
            return _policyRepository.GetAll().ToList();
        }

        public Policy GetPolicy(int policyId)
        {
            return _policyRepository.GetById(policyId);
        }

        public int GetNextPolicyId()
        {
            return _policyRepository.GetNextId();
        }

        public void AddPolicy(Policy policy)
        {
            _policyRepository.Add(policy);
        }

        public void DeletePolicy(Policy policy)
        {
            _policyRepository.Remove(policy);
        }

        public void AddCategory(string category)
        {
            _policyRepository.AddCategory(category);
        }

        public void EditCategory(int index, string newCategory)
        {
            _policyRepository.EditCategory(index, newCategory);
        }

        public void DeleteCategory(string category)
        {
            _policyRepository.RemoveCategory(category);
        }

        public IEnumerable<Company> ListAllCompanies()
        {
            return _companyRepository.GetAll();
        }
        
        public void EditStatus(int companyId, int applicationId, ApplicationStatus status)
        {
            _companyRepository.EditStatus(companyId, applicationId, status);
        }

        public void AddJobOpening(string jobTitle, int compId)
        {
            Company company = GetCompany(compId);
            var jobs = GetAllJobs().ToList();
            foreach (var job in jobs)
            {
                if (job.Title == jobTitle)
                {
                    company.Openings.Add(GetJob(job.JobId));
                    return;
                }
            }

            _jobRepository.Add(jobTitle);
            jobs = GetAllJobs().ToList();
            Job newJob = jobs.First(j => j.Title == jobTitle);
            company.Openings.Add(newJob);
            return;
        }

        public void RemoveOpening(int compId, int jobId)
        {
            Job job = GetJob(jobId);
            _companyRepository.DeleteOpening(compId, job);
        }

        public void AddNewEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }

        public IEnumerable<Employee> GetEmployeesByCompany(int compId)
        {
            return _employeeRepository.GetCompany(compId);
        }

        public Employee GetEmployee(int empId)
        {
            return _employeeRepository.Get(empId);
        }
    }
}
