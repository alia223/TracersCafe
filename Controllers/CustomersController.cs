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
            //initialise context so that Database can be modified
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //Dispose context once finished
            context.Dispose();
        }

        //GET: List of Customers
        [HttpGet]
        public ActionResult Index()
        {
            //Get the customers from the customer model and form a list
            List<CustomerModel> customers = context.Customers.ToList();
            return View(customers);
        }

        //GET: Customer Details Form
        [HttpGet]
        public ActionResult Create()
        {
            //Create a new ViewModel and return this ViewModel for
            //users to enter details into form
            CustomerModel customerModel = new CustomerModel()
            {
                //Initialise dropdown list object for use in the ViewModel
                //i.e. users will be able to see a drop down list on 
                //Create Customer Details Form which will contain these 4 values
                TitleList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Mr", Value = "Mr" },
                    new SelectListItem() { Text = "Miss", Value = "Miss" },
                    new SelectListItem() { Text = "Ms", Value = "Ms" },
                    new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                }
            };
            return View(customerModel);
        }

        //POST: Save New Customer Details To Database
        [HttpPost]
        public async Task<ActionResult> Store(CustomerModel customerModel)
        {
            //If there are any errors (according to the valdiation rules set in CustomerModel.cs
            //Then redirect user to a new customerViewModel with the appropriate validation messages on it
            //E.g. Firstname field is required
            if (!ModelState.IsValid)
            {
                CustomerModel newCustomerModel = new CustomerModel()
                {
                    TitleList = new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "Mr", Value = "Mr" },
                        new SelectListItem() { Text = "Miss", Value = "Miss" },
                        new SelectListItem() { Text = "Ms", Value = "Ms" },
                        new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                    }
                };
                return View("Create", newCustomerModel);
            }
            //Save to database
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == customerModel.Id);
            //customer already exists in database so just update the details
            if (customer != null)
            {
                customer.Title = customerModel.Title;
                customer.Firstname = customerModel.Firstname;
                customer.Surname = customerModel.Surname;
                customer.Address1 = customerModel.Address1;
                customer.Address2 = customerModel.Address2;
                customer.Address3 = customerModel.Address3;
                customer.Address4 = customerModel.Address4;
                customer.PostCode = customerModel.PostCode;
                customer.Telephone = customerModel.Telephone;
                customer.Age = customerModel.Age;
            }
            //customer does not exist in database so add a new record
            else
            {
                context.Customers.Add(new CustomerModel() {
                    Title = customerModel.Title,
                    Firstname = customerModel.Firstname,
                    Surname = customerModel.Surname,
                    Address1 = customerModel.Address1,
                    Address2 = customerModel.Address2,
                    Address3 = customerModel.Address3,
                    Address4 = customerModel.Address4,
                    PostCode = customerModel.PostCode,
                    Telephone = customerModel.Telephone,
                    Age = customerModel.Age
                });
            }
            //Save Customer Details asynchronously
            await context.SaveChangesAsync();
            return Redirect("/Customers");
        }

        //GET: Edit Customer Details Form
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get the customer model belonging to the selected customer
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == id);
            //Set the values of the CustomerViewModel edit form based on values stored in database/CustomerModel in line above 
            CustomerModel customerModel = new CustomerModel()
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
            //Depending on the value stored in the Title field of the database table for this customer
            //Set the appropriate drop down list item as selected
            //E.g. when creating form customer might select their title as 'Mr'
            //Therefore, when editing the details of this customer, the Title field should be set to 'Mr' initially
            customerModel.TitleList.Where(c => c.Text == customer.Title).FirstOrDefault().Selected = true;
            return View(customerModel);
        }

        //POST: Save Updated Customer Details To Database
        [HttpPost, ActionName("Update")]
        public async Task<ActionResult> Update(CustomerModel customerModel)
        {
            //if there are any errors according to the CustomerModel validation rules then return the new CustomerViewModel
            //with the appropriate validation messages as set in the CustomerModel
            if (!ModelState.IsValid)
            {
                CustomerModel newCustomerModel = new CustomerModel()
                {
                    Title = customerModel.Title,
                    TitleList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Mr", Value = "Mr" },
                    new SelectListItem() { Text = "Miss", Value = "Miss" },
                    new SelectListItem() { Text = "Ms", Value = "Ms" },
                    new SelectListItem() { Text = "Mrs", Value = "Mrs" }
                },
                    Firstname = customerModel.Firstname,
                    Surname = customerModel.Surname,
                    Address1 = customerModel.Address1,
                    Address2 = customerModel.Address2,
                    Address3 = customerModel.Address3,
                    Address4 = customerModel.Address4,
                    PostCode = customerModel.PostCode,
                    Telephone = customerModel.Telephone,
                    Age = customerModel.Age,
                };
                return View("Edit", newCustomerModel);
            }
            //Get selected customer
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == customerModel.Id);
            //customer already exists in database so just update the details
            if (customer != null)
            {
                customer.Title = customerModel.Title;
                customer.Firstname = customerModel.Firstname;
                customer.Surname = customerModel.Surname;
                customer.Address1 = customerModel.Address1;
                customer.Address2 = customerModel.Address2;
                customer.Address3 = customerModel.Address3;
                customer.Address4 = customerModel.Address4;
                customer.PostCode = customerModel.PostCode;
                customer.Telephone = customerModel.Telephone;
                customer.Age = customerModel.Age;
            }
            //customer does not exist in database so add a new record
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
            //Get selected customer
            CustomerModel customer = context.Customers.SingleOrDefault(c => c.Id == id);
            //Delete customer
            context.Entry(customer).State = EntityState.Deleted;
            //Save change asynchronously
            await context.SaveChangesAsync();
            return Redirect("/Customers");
        }
    }
}