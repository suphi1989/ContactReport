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
            return new List<string>() { "1", "2", "3" };
        }
    }
}
