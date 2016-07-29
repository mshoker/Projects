using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models;

namespace HRPortal.Data.Interfaces
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAll();
        Job Get(int id);
        int GetNextId();
        void Add(string title);
    }
}
