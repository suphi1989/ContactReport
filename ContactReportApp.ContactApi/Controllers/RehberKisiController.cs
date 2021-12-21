using ContactReportApp.ContactApi.Models;
using ContactReportApp.ContactApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RehberKisiController : ControllerBase
    {
        private readonly ILogger<RehberKisiController> _logger;
        private readonly ContactDBContext _context;
        public RehberKisiController(ILogger<RehberKisiController> logger, ContactDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Route("KisilerGetir")]
        [HttpGet]
        public HttpResponseMessage KisilerGetir()
        {
            try
            {
                string result = "";
                
                var kisiler = _context.Kisiler.ToList();

                if (kisiler != null)
                {
                    foreach (var kisi in kisiler)
                    {
                        var iletisimBilgileri = _context.IletisimBilgileri.Where(x => x.KisiId == kisi.Id).ToList();
                        
                        //foreach (var iletisimBilgi in iletisimBilgileri)
                        //{
                        //    kisi.IletisimBilgileri.Add(new IletisimBilgisi()
                        //    {
                        //        Id = iletisimBilgi.Id,
                        //        BilgiTipi = iletisimBilgi.BilgiTipi,
                        //        BilgiIcerigi = iletisimBilgi.BilgiIcerigi,
                        //        KisiId= iletisimBilgi.KisiId,
                        //        Kisi = kisi
                        //    });
                        //}
                    }
                    result = JsonConvert.SerializeObject(kisiler);
                }

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(result, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ex.Message, Encoding.UTF8, "application/json")
                };
            }

        }

          
        
    }
}
