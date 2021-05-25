using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsPro.BLL;
using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportsPro.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index() //display register page
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user) //register user & insert into db using POST method
        {
            try
            {
                var users = RegManager.CheckUser(user);
                if (users == null)
                {
                    //call the RegManager to add
                    RegManager.Add(user);

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return View();
                }
                   
            }
            catch
            {
                return RedirectToAction("Index", "Register");
            }

        }
}}
