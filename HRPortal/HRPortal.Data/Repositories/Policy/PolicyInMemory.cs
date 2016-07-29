using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Data.Interfaces;

namespace HRPortal.Data.Repositories.Policy
{
    public class PolicyInMemory : IPolicyRepository
    {
        private static List<Models.Policy> _policies;
        private static List<string> _categories;

        static PolicyInMemory()
        {
            _categories = new List<string>()
            {
                "Legal",
                "General",
                "Magical"
            };
            _policies = new List<Models.Policy>()
            {
                new Models.Policy()
                {
                    PolicyId = 1,
                    DateCreated = DateTime.Parse("9/9/1999"),
                    Name = "At will employment",
                    Description =
                        "Employee can be dismissed by an employer for any reason and without warning. Employee can leave employment for any reason and without warning.",
                    Category = "Legal"
                },

                new Models.Policy()
                {
                    PolicyId = 3,
                    DateCreated = DateTime.Parse("9/10/1999"),
                    Name = "At RISK",
                    Description =
                        "Employee can be subject to bad stuff Without warning.",
                    Category = "Legal"
                },

                new Models.Policy()
                {
                    PolicyId = 2,
                    DateCreated = DateTime.Parse("1/1/1000"),
                    Name = "General Safety",
                    Description = "Keep your wits about you",
                    Category = "General"
                }

            };
        }
        public IEnumerable<Models.Policy> GetAll()
        {
            return _policies;
        }

        public IEnumerable<Models.Policy> GetByCategory(string category)
        {
            return _policies.Where(p => p.Category == category);
        }

        public Models.Policy GetById(int id)
        {
            return _policies.FirstOrDefault(x => x.PolicyId == id);
        }

        public int GetNextId()
        {
            return (_policies.Select(x => x.PolicyId).Max() + 1);
        }

        public void Add(Models.Policy policy)
        {
            policy.DateCreated = DateTime.Now;
            policy.PolicyId = GetNextId();
            _policies.Add(policy);
        }

        public void Remove(Models.Policy policy)
        {
            _policies.Remove(policy);
        }

        public void Edit(Models.Policy policy)
        {
            _policies.Remove(policy);
        }

        public List<string> GetCategories()
        {
            return _categories;
        }

        public void AddCategory(string category)
        {
            _categories.Add(category);
        }

        public void RemoveCategory(string category)
        {
            List<Models.Policy> policiesToRemove = new List<Models.Policy>();
            foreach (var policy in _policies)
            {
                if (policy.Category == category)
                {
                    policiesToRemove.Add(policy);
                }
            }

            foreach (var p in policiesToRemove)
            {
                _policies.Remove(p);
            }
            _categories.Remove(category);
        }

        public void EditCategory(int index, string newCategory)
        {
            foreach (var policy in _policies)
            {
                if (policy.Category == _categories[index])
                {
                    policy.Category = newCategory;
                }
            }
            _categories[index] = newCategory;
        }
    }
}
