using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Context;
using Movie.Context.Repository;
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
        private FilmRepository _filmRepository;
        private SerialRepository _serialRepository;

        private MovieContext mc;

        public AdminPanelController(MovieContext movieContext)
        {
            mc = movieContext;

            _filmRepository = new FilmRepository(movieContext);
            _serialRepository = new SerialRepository(movieContext);
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

            var modelData = JsonConvert.DeserializeAnonymousType(model.ToString(), new { id = 0, type = "" });

            switch (modelData.type)
            {
                case "Film":
                    mc.Films.Remove(await mc.Films.FirstAsync(x => x.Id == modelData.id));
                    break;
                case "Serial":
                    mc.Serials.Remove(await mc.Serials.FirstAsync(x => x.Id == modelData.id));
                    break;
                default:
                    //Error
                    break;
            }
            await mc.SaveChangesAsync();
            return new JsonResult(Ok());
        }
        public async Task<IActionResult> Edit([FromBody] object model)
        {
            try
            {
                var modelData = JsonConvert.DeserializeAnonymousType(model.ToString(), new { type = "", newModel = new Object() });
                switch (modelData.type)
                {
                    case "Film":
                        Film f = JsonConvert.DeserializeObject<Film>(modelData.newModel.ToString());
                        _filmRepository.Update(f.Id, f);
                        break;
                    case "Serial":
                        Serial s = JsonConvert.DeserializeObject<Serial>(modelData.newModel.ToString());
                        _serialRepository.Update(s.Id, s);
                        break;

                    default:
                        //Error
                        break;
                }
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
           





        }

        [HttpPost]
        public IActionResult getPaintings([FromBody] object model)
        {
            string JsonList = "";
            switch (model.ToString())
            {
                case "Film":
                    JsonList = JsonConvert.SerializeObject(mc.Films.ToList());
                    break;
                case "Serial":
                    JsonList = JsonConvert.SerializeObject(mc.Serials.ToList());
                    break;
                default:
                    return new JsonResult("Error: Type not found!")
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
            }

            return new JsonResult(JsonList);
        }

    }
}
