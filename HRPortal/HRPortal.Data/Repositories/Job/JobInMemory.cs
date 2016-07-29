using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;

namespace HRPortal.Data.Repositories.Job
{
    class JobInMemory : IJobRepository
    {
        private static List<Models.Job> _jobs;

        static JobInMemory()
        {
            //sample data
            _jobs = new List<Models.Job>()
            {
                new Models.Job()
                {
                    JobId = 1,
                    Title = "Sales Manager"
                },
                new Models.Job()
                {
                    JobId = 2,
                    Title = "Auror"
                },
                new Models.Job()
                {
                    JobId = 3,
                    Title = "Pirate"
                }
            };
        }
        public IEnumerable<Models.Job> GetAll()
        {
            return _jobs;
        }

        public Models.Job Get(int id)
        {
            return _jobs.FirstOrDefault(j => j.JobId == id);
        }

        public int GetNextId()
        {
            return (_jobs.Select(i => i.JobId).Max() + 1);
        }

        public void Add(string title)
        {
            int newId = GetNextId();
            Models.Job newJob = new Models.Job()
            {
                JobId = newId,
                Title = title
            };

            _jobs.Add(newJob);
        }

       
    }
}
