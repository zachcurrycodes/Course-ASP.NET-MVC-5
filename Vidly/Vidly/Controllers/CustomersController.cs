using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult New() {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer) {
            if (customer.Id == 0) {
                _context.Customers.Add(customer);
            } else {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Microsoft wants us to use this, this is the override method
                // TryUpdateModel(customerInDb, "", new string[] {"Name", "Email"});

                // another
                // Can use a library like AutoMapper
                // create new class with a specific dataStructure (Dto = dataTransferObject) with only properties that can be updated
                // Mapper.Map(customer, customerInDb); public ActionResult Save(UpdateCustomerDto customer)

                // Course recommends the following way
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Index() {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id) {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null) {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult Edit(int id) {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel() {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}