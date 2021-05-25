﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SportsPro.Controllers
{ 
    public class AccountController : Controller
    {
        private readonly SportsProContext _context;

        public AccountController(SportsProContext context)
        {
            _context = context;
        }
        

        public IActionResult Login(string returnUrl = null)
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(Users user)
        {
            //authenticate using the manager
            var usr = _context.Users.SingleOrDefault(u => u.Username == user.Username && user.Password == user.Password);
            //return now if user object returned is null
            if (usr == null) { var error = "Invalid"; return View(error); }
            //otherwise set up claims--one for each fact about the user

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usr.Username),
                new Claim("FullName", usr.FullName),
                new Claim(ClaimTypes.Role, usr.Role)
            };
            //create the instance of ClaimsIdentity (holds the claims)
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            //The signin creates the ClaimsPrincipal and cookie
            await HttpContext.SignInAsync("Cookies",
                                          new ClaimsPrincipal(claimsIdentity));
            //handle the return url value from TempData if it exists or not
            if (TempData["ReturnUrl"] == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(TempData["ReturnUrl"].ToString());
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
