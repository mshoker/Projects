using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRPortal.BLL;
using HRPortal.Data.Repositories;
using HRPortal.Data.Repositories.Policy;
using HRPortal.Models;

namespace HRPortal.Controllers
{
    public class PolicyController : Controller
    {
        Operations ops = new Operations(RepositoryFactory.Policy(), RepositoryFactory.Company(), RepositoryFactory.Job(), RepositoryFactory.Employee());


        //public PolicyController()
        //{
        //    ops = new AdminFunctions(PolicyFactory.Create());
        //}

        // GET: Policy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectViewCategory()
        {
            var pcvm = new PolicyCategoryVM();
            var categories = ops.ListCategories();
            pcvm.Policies = ops.ListPolicies();
            pcvm.SetCategoryItems(categories);
            return View(pcvm);
        }
        
        public ActionResult ManagePolicies()
        {
            
            List<string> categories = ops.ListCategories();
            PolicyCategoryVM pcvm = new PolicyCategoryVM();
            pcvm.SetCategoryItems(categories);
            pcvm.Policies = ops.ListPolicies();

            return View(pcvm);
        }

        public ActionResult ManageCategories()
        {
            
            List<string> categories = ops.ListCategories();
            PolicyCategoryVM pcvm = new PolicyCategoryVM();
            pcvm.SetCategoryItems(categories);
            pcvm.Policies = ops.ListPolicies();
            pcvm.Origin = "newCategory";

            return View(pcvm);
        }

        public ActionResult Add()
        {
            
            PolicyCategoryVM pcvm = new PolicyCategoryVM();
            pcvm.SetCategoryItems(ops.ListCategories());
            pcvm.Policies.Add(new Policy());
            pcvm.Origin = "newPolicy";
            return View(pcvm);
        }

        [HttpPost]
        public ActionResult Add(PolicyCategoryVM pcvm)
        {
            
            Policy newPolicy = pcvm.Policies.First();
            newPolicy.Category = ops.ListCategories()[int.Parse(newPolicy.Category)];
            newPolicy.DateCreated = DateTime.Now;
            newPolicy.PolicyId = ops.GetNextPolicyId();
            ops.AddPolicy(newPolicy);

            return RedirectToAction("ManagePolicies", "Policy");
        }

        
        public ActionResult Delete(int policyId)
        {
            
            Policy policy = ops.GetPolicy(policyId);
            return View(policy);
        }

        [HttpPost]
        public ActionResult Delete(Policy policy)
        {
            
            Policy policyDelete = ops.GetPolicy(policy.PolicyId);
            ops.DeletePolicy(policyDelete);
            return RedirectToAction("ManagePolicies");
        }

        
        public ActionResult AddCategory(string origin)
        {
            NewCategoryVM ncvm = new NewCategoryVM();
            ncvm.Origin = origin;
            return View(ncvm);
        }

        [HttpPost]
        public ActionResult AddCategory(NewCategoryVM ncvm)
        {
            
            ops.AddCategory(ncvm.Category);
            if (ncvm.Origin == "newPolicy")
            {
                return RedirectToAction("Add");
            }
            else
            {
                return RedirectToAction("ManageCategories");
            }
            
        }

        public ActionResult EditCategory(string editCategory)
        {
            NewCategoryVM ncvm = new NewCategoryVM();
            ncvm.Category = editCategory;
            return View(ncvm);
        }

        [HttpPost]
        public ActionResult EditCategory(NewCategoryVM ncvm)
        {
            
            
            var list = ops.ListCategories();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == ncvm.Category)
                {
                    int index = i;
                    ops.EditCategory(index, ncvm.Origin);
                }
            }
            return RedirectToAction("ManageCategories");
        }

        public ActionResult DeleteCategory(string deleteCategory)
        {
            PolicyCategoryVM pcvm = new PolicyCategoryVM();
            pcvm.Origin = deleteCategory;
            
            var policies = ops.ListPolicies().Where(p => p.Category == deleteCategory);
            foreach (var policy in policies)
            {
                pcvm.Policies.Add(policy);
            }

            pcvm.Categories = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = deleteCategory,
                    Value = deleteCategory
                }
            };
            return View(pcvm);
        }

        [HttpPost]
        public ActionResult DeleteCategory(PolicyCategoryVM pcvm)
        {
            
            ops.DeleteCategory(pcvm.Origin);
            return RedirectToAction("ManageCategories");
        }
    }
}