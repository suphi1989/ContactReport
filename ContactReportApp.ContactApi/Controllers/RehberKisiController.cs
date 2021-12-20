using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RehberKisiController : ControllerBase
    {
        private readonly ILogger<RehberKisiController> _logger;
        public RehberKisiController(ILogger<RehberKisiController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<string> Get()
        {
            //var connection = new Npgsql.NpgsqlConnection("Server=localhost;Database=postgres;Port=5432;User Id=postgres;Password=1234;Pooling=false;");
            //connection.Open();
            return new List<string>() { "1", "2", "3" };
        }
    }
}
