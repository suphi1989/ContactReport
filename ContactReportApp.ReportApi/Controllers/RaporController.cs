using ContactReportApp.ReportApi.Entities;
using ContactReportApp.ReportApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaporController : ControllerBase
    {
        private readonly ILogger<RaporController> _logger;
        private readonly ReportDBContext _context;
        public RaporController(ILogger<RaporController> logger, ReportDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Route("RaporlarGetir")]
        [HttpGet]
        public ActionResult<List<Rapor>> RaporlarGetir()
        {
            try
            {
                var RaporListesi = _context.Raporlar.ToList();
                
                if (RaporListesi != null && RaporListesi.Count > 0)
                    return Ok(RaporListesi);
                else 
                    return NotFound("Rapor listesi bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [Route("RaporlarGetir")]
        [HttpPost]
        public ActionResult<int> RaporOlustur()
        {
            try
            {
                var rapor = new Rapor();

                //Rapor Oluştur
                Task t1 = Task.Run(() =>
                {
                    rapor.DosyaYolu = "";
                    rapor.RaporDurumu = RaporDurum.Hazirlaniyor;
                    rapor.RaporTarihi = DateTime.Now;

                    _context.Add(rapor);

                    _context.SaveChanges();
                });

                //Rapor detayları almak için Contact Apiye istek gönder...Contact api de Kafka queue'ya rapor json olarak gönderecek. 
                Task t2 = Task.Run(() =>
                {

                });

                return Ok(rapor.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
