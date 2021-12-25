using ContactReportApp.ReportApi.Entities;
using ContactReportApp.ReportApi.Models;
using ContactReportApp.ReportApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        
        [Route("RaporOlustur")]
        [HttpPost]
        public ActionResult<int> RaporOlustur(string Konum)
        {
            try
            {
                //Rapor Oluştur

                int result = 0;
                var rapor = new Rapor();
                rapor.DosyaYolu = "";
                rapor.RaporDurumu = RaporDurum.Hazirlaniyor;
                rapor.RaporTarihi = DateTime.Now;
                _context.Add(rapor);
                _context.SaveChanges();
                result = rapor.Id;

                //Rapor detayları almak için Contact Apiye istek gönder...Contact api den Kafka queue'ya rapor json olarak gönderecek. 

                Task t1 = Task.Run(() =>
                {
                    var restApi = new RestClient("https://localhost:44305"); // Contact Api url
                    var request = new RestRequest("RehberKisi/KisilerKonumaGoreRaporuOlustur", Method.GET);
                    request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
                    request.AddParameter("Konum", Konum);
                    request.AddParameter("RaporId", rapor.Id);
                    request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
                    var response = restApi.Execute(request);
                });

            
                /* Thread.Sleep(5000);
                 bool done = t1.IsCompleted;*/

                return Ok("Rapor isteği başarıyla gönderildi. RaporId:" + result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("RaporStatuGuncelle")]
        [HttpPost]
        public ActionResult<bool> RaporStatuGuncelle([FromBody] RaporStatuModel raporStatuModel)
        {
            try
            {

                var rapor = _context.Raporlar.FirstOrDefault(x => x.Id == int.Parse(raporStatuModel.RaporId));

                if (rapor != null)
                {
                    rapor.DosyaYolu = raporStatuModel.DosyaYolu;
                    rapor.RaporDurumu = raporStatuModel.RaporDurumu;
                    _context.SaveChanges();
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }
    }
}
