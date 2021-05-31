using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Dzianis Tsishchenka
/// </summary>

namespace SportsPro.BLL
{
    public class RegManager
    {
       
         
        public static Users CheckUser(Users user) //check if user registered already in the system
        {
            var db = new SportsProContext();
            var users = db.Users.SingleOrDefault(i => i.Username == user.Username && i.Password == user.Password);
            return users;
        }

        

        public static void Add(Users user) //add user to db
        {
            var context = new SportsProContext();
            context.Users.Add(user);
            context.SaveChanges();
        }

    }
}
