using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET api/files/sample.png
        [HttpGet("{fileName}")]
        public string Get(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "/images/" + fileName;
            byte[] b = System.IO.File.ReadAllBytes(path);
            //return File(new FileStream('path to file',FileAccess.Read), "application/pdf", "filename.pdf");
            
            return string.Empty;
        }

        class ObjectSamepl
        {
            public long Id { get; set; }
            public List<ObjectSamepl> DataInside { get; set; }
        }

        [HttpGet("checkdataTable")]
        public DataTable Dt()
        {
            List<ObjectSamepl> lst = new List<ObjectSamepl>
            {
               new ObjectSamepl
               { 
                   Id = 0, 
                   DataInside = new List<ObjectSamepl>()
                   { 
                       new ObjectSamepl {  Id = 2, DataInside = null } 
                   } 
               },
                new ObjectSamepl
               {
                   Id = 0,
                   DataInside = new List<ObjectSamepl>()
                   {
                       new ObjectSamepl {  Id = 2, DataInside = null }
                   }
               }

            };

            //error right here
            string _d = JsonConvert.SerializeObject(lst);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(_d);

            return dt;
        }



    }
}
