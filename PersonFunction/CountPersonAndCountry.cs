using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Data;
using System.Collections.Generic;
using Application.Models;

namespace PersonFunction
{
    public class CountPersonAndCountry
    {
        PersonDbContext _context;

        public CountPersonAndCountry(PersonDbContext context)
        {
            _context = context;
        }

        [FunctionName("CountPersonAndCountry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Friend Function");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);


            var person = new List<Person>();

            var country = new List<Countries>();


            //person = _context.Friends.FromSqlRaw("EXEC CountPerson").ToList();

            //country = _context.Country.FromSqlRaw("EXEC CountCountry").ToList();



            string responseMessage = $"The number of friends are " +
                $" || The number of countries are ";


            return new OkObjectResult(responseMessage);
        }
    }
}
