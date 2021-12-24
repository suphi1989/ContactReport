using Confluent.Kafka;
using ContactReportApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ConsumerConfig _config;
        static volatile public bool isStartConsume;

        public HomeController(ILogger<HomeController> logger, ConsumerConfig config)
        {
            _logger = logger;
            _config = config;

            if (!isStartConsume)
            {
                var kafkaConsumer = new KafkaConsumer(_config);
                kafkaConsumer.StartConsumerAsync();
            }
            isStartConsume = true;
        }

        public IActionResult Index()
        {

           

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
