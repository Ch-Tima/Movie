using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Context;
using Movie.Helpers;
using Movie.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
 

namespace Movie.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly MovieContext mc;
        public AdminPanelController(MovieContext movieContext)
        {
            mc = movieContext;
        }
        public IActionResult Index(string error = null) => View(error);

        [HttpPost]
        public IActionResult SignIn(string Login, string Password)
        {
            Models.Admin admin = mc.Admins.FirstOrDefault(x => x.Login == Login && x.Password == HashPasswordHelpers.HashPassword(Password));


            if (admin == null)
                return View(nameof(Index), "Error: User not found!");
            else
                return View(nameof(Panel), mc.Films.ToList());
        }
        [HttpPost]
        public IActionResult Panel() => View();
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] object model) 
        {
            mc.Films.Remove(await mc.Films.FirstAsync(x => x.Id == int.Parse(model.ToString())));
            await mc.SaveChangesAsync();
            return new JsonResult(Ok());
        }
        public async Task<IActionResult> Edit([FromBody] object model)
        {
            try
            {
                //Todo: Rewrite
                var t = JsonConvert.DeserializeObject<Film>(model.ToString());
                mc.Films.Remove(mc.Films.First(x => x.Id == t.Id));
                mc.Films.Add(t);
                await mc.SaveChangesAsync();

                return new JsonResult(model);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        
    }
}
