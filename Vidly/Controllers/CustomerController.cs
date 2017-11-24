using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context; 
        public CustomerController()
            {
            _context = new ApplicationDbContext();
            }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public ActionResult Create( Customer customer)
        {

            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("New", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);

            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        public ActionResult New()
        { 

            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                Customer= new Customer() ,
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);

        }
        public ActionResult Detail(int? id)

        {
            var customer = _context.Customers.Include(e =>e.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer ==null )
            {
                return HttpNotFound(); 
            }      
            return View(customer);
        }
        public ActionResult Edit(int? id)

        {
            var customer = _context.Customers.Include(e => e.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
           
            
                var viewModel = new NewCustomerViewModel
                {
                    Customer= customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
            
            return View("New", viewModel);
        }
    }
}