using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TracersCafe.Models;

namespace TracersCafe.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //GET: List of Customers
        [HttpGet]
        public ActionResult Index()
        {
            List<CustomerModel> customers = context.Customers.ToList();
            return View(customers);
        }

        //GET: Customer Details Form
        [HttpGet]
        public ActionResult Create()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                TitleList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Mr", Value = "Mr" },
                    new SelectListItem() { Text = "Miss", Value = "Miss" },
                    new SelectListItem() { Text = "Ms", Value = "Ms" },
                    new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                }
            };
            return View(customerViewModel);
        }

        //POST: Save New Customer Details To Database
        [HttpPost]
        public async Task<ActionResult> Store(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                CustomerViewModel newCustomerViewModel = new CustomerViewModel()
                {
                    TitleList = new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "Mr", Value = "Mr" },
                        new SelectListItem() { Text = "Miss", Value = "Miss" },
                        new SelectListItem() { Text = "Ms", Value = "Ms" },
                        new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                    }
                };
                return View("Create", newCustomerViewModel);
            }
            //Save to database
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == customerViewModel.Id);
            //customer already exists so just update the details
            if (customer != null)
            {
                customer.Title = customerViewModel.Title;
                customer.Firstname = customerViewModel.Firstname;
                customer.Surname = customerViewModel.Surname;
                customer.Address1 = customerViewModel.Address1;
                customer.Address2 = customerViewModel.Address2;
                customer.Address3 = customerViewModel.Address3;
                customer.Address4 = customerViewModel.Address4;
                customer.PostCode = customerViewModel.PostCode;
                customer.Telephone = customerViewModel.Telephone;
                customer.Age = customerViewModel.Age;
            }
            else
            {
                context.Customers.Add(new CustomerModel() {
                    Title = customerViewModel.Title,
                    Firstname = customerViewModel.Firstname,
                    Surname = customerViewModel.Surname,
                    Address1 = customerViewModel.Address1,
                    Address2 = customerViewModel.Address2,
                    Address3 = customerViewModel.Address3,
                    Address4 = customerViewModel.Address4,
                    PostCode = customerViewModel.PostCode,
                    Telephone = customerViewModel.Telephone,
                    Age = customerViewModel.Age
                });
            }
            //Save Customer Details
            await context.SaveChangesAsync();
            return Redirect("/Customers");
        }

        //GET: Edit Customer Details Form
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == id);
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Title = customer.Title,
                TitleList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Mr", Value = "Mr" },
                    new SelectListItem() { Text = "Miss", Value = "Miss" },
                    new SelectListItem() { Text = "Ms", Value = "Ms" },
                    new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                },
                Firstname = customer.Firstname,
                Surname = customer.Surname,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                Address3 = customer.Address3,
                Address4 = customer.Address4,
                PostCode = customer.PostCode,
                Telephone = customer.Telephone,
                Age = customer.Age,

            };
            customerViewModel.TitleList.Where(c => c.Text == customer.Title).FirstOrDefault().Selected = true;
            return View(customerViewModel);
        }

        //POST: Save Updated Customer Details To Database
        [HttpPost, ActionName("Update")]
        public async Task<ActionResult> Update(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                CustomerViewModel newCustomerViewModel = new CustomerViewModel()
                {
                    Title = customerViewModel.Title,
                    TitleList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Mr", Value = "Mr" },
                    new SelectListItem() { Text = "Miss", Value = "Miss" },
                    new SelectListItem() { Text = "Ms", Value = "Ms" },
                    new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                },
                    Firstname = customerViewModel.Firstname,
                    Surname = customerViewModel.Surname,
                    Address1 = customerViewModel.Address1,
                    Address2 = customerViewModel.Address2,
                    Address3 = customerViewModel.Address3,
                    Address4 = customerViewModel.Address4,
                    PostCode = customerViewModel.PostCode,
                    Telephone = customerViewModel.Telephone,
                    Age = customerViewModel.Age,
                };
                return View("Edit", newCustomerViewModel);
            }
            //Save to database
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == customerViewModel.Id);
            //customer already exists so just update the details
            if (customer != null)
            {
                customer.Title = customerViewModel.Title;
                customer.Firstname = customerViewModel.Firstname;
                customer.Surname = customerViewModel.Surname;
                customer.Address1 = customerViewModel.Address1;
                customer.Address2 = customerViewModel.Address2;
                customer.Address3 = customerViewModel.Address3;
                customer.Address4 = customerViewModel.Address4;
                customer.PostCode = customerViewModel.PostCode;
                customer.Telephone = customerViewModel.Telephone;
                customer.Age = customerViewModel.Age;
            }
            else
            {
                context.Customers.Add(new CustomerModel() {
                    Title = customer.Title,
                    Firstname = customer.Firstname,
                    Surname = customer.Surname,
                    Address1 = customer.Address1,
                    Address2 = customer.Address2,
                    Address3 = customer.Address3,
                    Address4 = customer.Address4,
                    PostCode = customer.PostCode,
                    Telephone = customer.Telephone,
                    Age = customer.Age,
                });
            }
            //Save Customer Details
            await context.SaveChangesAsync();
            return Redirect("/Customers");
        }

        //DELETE: Delete Customer/Customer Details
        public async Task<ActionResult> Delete(int id)
        {
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == id);
            context.Entry(customer).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return Redirect("/Customers");
        }
    }
}