using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Mark Tierney
/// </summary>

namespace SportsPro.BLL
{
    public class CustomerManager
    {

        public static void update(Customers customer) //update customer
        {

            var context = new SportsProContext();
            var originalCustomer = context.Customers.Find(customer.CustomerId);
            originalCustomer.FirstName = customer.FirstName;
            originalCustomer.LastName = customer.LastName;
            originalCustomer.Email = customer.Email;
            originalCustomer.Address = customer.Address;
            originalCustomer.City = customer.City;
            originalCustomer.State = customer.State;
            originalCustomer.PostalCode = customer.PostalCode;
            originalCustomer.Country = customer.Country;
            originalCustomer.Phone = customer.Phone;
            context.SaveChanges();
        }

        public static Customers Find(int id) //find customer by id
        {
            var context = new SportsProContext();
            var customer = context.Customers.Find(id);
            return customer;
        }

    }
}
