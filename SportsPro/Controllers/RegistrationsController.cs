using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegistrationsController : Controller
    {
        private readonly SportsProContext _context;

        public RegistrationsController(SportsProContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId",  "FullName");//assign context.Customers to ViewData
                                                                                                   //in order to disply in dropdown list
            return View();
        }


        public IActionResult Registrations()
        {
            try //customer selected case
            {
                var customerID = Convert.ToInt32(Request.Form["ddl"]); //get selected customerID from drop down list
                TempData["customerID"] = customerID.ToString();//store customerID in tempdata 
                var customers = RegistrationsManager.FindCustomerName(customerID);
                TempData["FullName"] = customers.FullName.ToString(); //store customer full name to TempData
                                                                      //display registered products by current customer
                var regProducts = RegistrationsManager.GetAllProductsByCustomer(customerID);
                //display product dropdown
                ViewBag.Products = RegistrationsManager.GetAllProducts();

                return View(regProducts);
            }
           
            catch //customer from session during redirection
            {
                var customerID = Convert.ToInt32(TempData["customersID"]); //retreive customerID from tempdata
                TempData["customerID"] = customerID.ToString();//store customerID in tempdata 
                var customers = RegistrationsManager.FindCustomerName(customerID);
                TempData["FullName"] = customers.FullName.ToString(); //store customer full name to TempData
                                                                      //display registered products by current customer
                var regProducts = RegistrationsManager.GetAllProductsByCustomer(customerID);
                //display product dropdown
                ViewBag.Products = RegistrationsManager.GetAllProducts();

                return View(regProducts);
            }

        }

        public IActionResult AddProduct()
        {
            var name = Convert.ToString(Request.Form["sel"]); //get selected product name from drop down list
            var products = RegistrationsManager.FindProductbyName(name); //FindProductby product name

            var customerID = Convert.ToInt32(TempData["customerID"]); //retreive customerID from tempdata
            TempData["customerID"] = customerID.ToString(); ////store customerID in tempdata to maintain selected customer state
            TempData["customersID"] = customerID.ToString();
            var customers = RegistrationsManager.FindCustomerName(customerID);
            TempData["FullName"] = customers.FullName.ToString(); //store customer full name to TempData
            var regProduct = new Registrations //create new regProduct object and assign properties values
            {
                 ProductName = name,
                 CustomerId = customerID
            };
            RegistrationsManager.AddRegProduct(regProduct); //insert regProduct into registrations db
            return RedirectToAction(nameof(Registrations));
        }


        // GET: registrations/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrations = await _context.Registrations
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (registrations == null)
            {
                return NotFound();
            }
            var customerID = Convert.ToInt32(TempData["customerID"]); //retreive customerID from tempdata
            TempData["customerID"] = customerID.ToString(); ////store customerID in tempdata to maintain selected customer state
            TempData["customersID"] = customerID.ToString();
            return View(registrations);
        }

        // POST: registrations/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrations = await _context.Registrations.FindAsync(id);
            _context.Registrations.Remove(registrations); //remove from reg product db
            await _context.SaveChangesAsync();
            var customerID = Convert.ToInt32(TempData["customerID"]); //retreive customerID from tempdata
            TempData["customerID"] = customerID.ToString(); ////store customerID in tempdata to maintain selected customer state
            TempData["customersID"] = customerID.ToString();
            return RedirectToAction(nameof(Registrations));
        }

    }
}
