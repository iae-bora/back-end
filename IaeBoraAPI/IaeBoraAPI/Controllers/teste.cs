using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using IaeBoraLibrary.Model.Enums;
using IaeBoraLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("v0/teste")]
    public class teste : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            Route rota = new Route();
            rota.RouteCategories.Add(PlacesEnum.Shopping);
            RouteService.SaveRoutesDetails(rota, null);

           //// List<IaeBoraLibrary.Model.place> x = new List<IaeBoraLibrary.Model.place>();
           // using (var context = new Context())
           // {
           //     // var x = context.Place.First();
           //     var a = context.Opening_Hours.First();
           //     //return context.Answers.Where(a => a.User.GoogleId == userId).Include("User").First();
           // }
           return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult BAL([FromBody] List<PlacesEnum> places)
        {
            var x = places;
            return Ok(x);
        }
    }
}
