using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using KeyPathFunctions.Models;
using KeyPathFunctions.DAL;
using Microsoft.EntityFrameworkCore;

namespace KeyPathFunctions
{
    public class CreateHero
    {

        private readonly KeyPathContext _dbContext;

        // Constructor injection for DbContext
        public CreateHero(KeyPathContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("CreateHero")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {

            string name = req.Query["Name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage;

            if (!string.IsNullOrEmpty(name))
            {
                //create the new hero
                HeroModel hero = new HeroModel();
                hero.Name = name;

                //add the hero to the context
                _dbContext.Add(hero);
                await _dbContext.SaveChangesAsync();

                responseMessage = $"The function executed successfully and the Hero {name} was created.";
            }
            else
            {
                responseMessage = "Please pass a name to create a new hero";
            }
 

            return new OkObjectResult(responseMessage);
        }
    }
}
