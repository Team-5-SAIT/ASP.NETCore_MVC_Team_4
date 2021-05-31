using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

/// <summary>
/// Author: Dzianis Tsishchenka
/// </summary>

namespace SportsPro.BLL
{
    public class RegistrationsManager
    {
        public static void AddRegProduct(Registrations regProduct) //insert product into db
        {
            var db = new SportsProContext();
            db.Registrations.Add(regProduct);
            db.SaveChanges();
        }

        public static Customers FindCustomerName(int customerID) //find customer name by customer id
        {
            var db = new SportsProContext();
            var customers = db.Customers.SingleOrDefault(i => i.CustomerId == customerID);
            return customers;
        }

        public static Products FindProductbyName(string name) //find selected product by name
        {
            var db = new SportsProContext();
            var products = db.Products.SingleOrDefault(i => i.Name == name);
            return products;
        }




        public static List<Registrations> GetAllProductsByCustomer(int customerID) //GetAllProductsByCustomer
        {
            var context = new SportsProContext();
            var regProducts = context.Registrations.Where(o => o.CustomerId== customerID).ToList();
            return regProducts;
        }

        public static List<Products> GetAllProducts() //GetAllProducts
        {
            var context = new SportsProContext();
            var products = context.Products.ToList();
            return products;
        }
    }
}
