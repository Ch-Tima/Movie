using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie.Context;
using Movie.Helpers;
using Movie.Models;
using System;
using System.Linq;
using System.Threading;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext mc;
        public HomeController(MovieContext movieContext)
        {
            mc = movieContext;

            //mc.Admins.Add(new Admin()
            //{
            //    Login = "Tima",
            //    Password = HashPasswordHelpers.HashPassword("Adm1nT1m@"),
            //});
            //mc.SaveChanges();


        }
        public IActionResult Index() => View(mc.Films.ToList());

        [HttpGet]
        public IActionResult PageMovie(int Id)
        {
            try
            {
                var m = mc.Films.First(x => x.Id == Id);
                return View(m);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
    }
}
