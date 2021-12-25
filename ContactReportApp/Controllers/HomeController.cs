using Confluent.Kafka;
using ContactReportApp.Api;
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
        private ContactClientApi _contactClientApi;
        private ReportClientApi _reportClientApi;


        static volatile public bool isStartConsume;

        public HomeController(ILogger<HomeController> logger, ConsumerConfig config)
        {
            _logger = logger;
            _config = config;
            _contactClientApi = new ContactClientApi();
            _reportClientApi = new ReportClientApi();

            if (!isStartConsume)
            {
                var kafkaConsumer = new KafkaConsumer(_config, _logger);
                kafkaConsumer.StartConsumerAsync();
            }
            isStartConsume = true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportList()
        {
            var raporlar = _reportClientApi.GetReportList();
            return View("ReportView", new ReportViewModel() { raporListesi = raporlar });
        }
        
        public IActionResult RaporuOlustur(ReportViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Arama))
            {
                string result = _reportClientApi.ReportCreate(model.Arama);
                TempData["ReportCreated"] = result;
            }
            var raporlar = _reportClientApi.GetReportList();
            return View("ReportView", new ReportViewModel() { raporListesi = raporlar });
        }
        
        public IActionResult RaporuKaldir(int RaporId)
        {
            string dosyaYolu1 = string.Format(@"excelfiles\rapor_{0}.xlsx", RaporId); 
            string dosyaYolu2 = string.Format(@"d:\logs\excelfiles\rapor_{0}.xlsx", RaporId);

            if(System.IO.File.Exists(dosyaYolu1))
                System.IO.File.Delete(dosyaYolu1);

            if (System.IO.File.Exists(dosyaYolu2))
                System.IO.File.Delete(dosyaYolu2);

            _reportClientApi.ReportDelete(RaporId);

            var raporlar = _reportClientApi.GetReportList();

            return View("ReportView", new ReportViewModel() { raporListesi = raporlar });
        }


        public IActionResult ContactList()
        {
            var kisiler = _contactClientApi.GetContactList();

            return View("ContactView", new ContactViewModel() { Kisiler = kisiler });
        }
        public IActionResult CreateContact()
        {
            return Ok();
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
