using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatFactsRestAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CatFactsAPIController : ControllerBase
    {
        [HttpGet("facts")]
        public IActionResult GetFacts()
        {
            //Arrange
            const string jsonFilePath = @".\StaticFiles\catfacts.json";
            string facts = System.IO.File.ReadAllText(jsonFilePath);
            var catFactsResult = FactsDTO.FromJson(facts);
            return new JsonResult(catFactsResult);
        }
    }
}
