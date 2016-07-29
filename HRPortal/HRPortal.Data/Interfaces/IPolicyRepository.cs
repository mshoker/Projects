using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models;

namespace HRPortal.Data.Interfaces
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetAll();
        IEnumerable<Policy> GetByCategory(string category);
        Policy GetById(int id);
        int GetNextId();
        void Add(Policy policy);
        void Remove(Policy policy);
        void Edit(Policy policy);
        List<string> GetCategories();
        void AddCategory(string category);
        void RemoveCategory(string category);
        void EditCategory(int index, string newCategory);
    }
}
