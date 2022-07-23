using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie.Context;
using Movie.Helpers;
using Movie.Interfis;
using Movie.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext mc;
        public HomeController(MovieContext movieContext)
        {
            mc = movieContext;
        }
        public IActionResult Index() => View(mc.Films.ToList());

        [HttpGet]
        public IActionResult PageMovie(string model)
        {
            try
            {
                var modelData = JsonConvert.DeserializeAnonymousType(model, new { id = 0, type = "" });

                string paintingData;

                switch (modelData.type)
                {
                    case "Film":
                        paintingData = "Film\n" + JsonConvert.SerializeObject(mc.Films.First(x => x.Id == modelData.id));
                        break;
                    case "Serial":
                        paintingData = "Serial\n" + JsonConvert.SerializeObject(mc.Serials.First(x => x.Id == modelData.id));
                        break;
                    default:
                        paintingData = null;
                       break;
                }
                var str = paintingData.Split('\n');
                return View((object)paintingData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
