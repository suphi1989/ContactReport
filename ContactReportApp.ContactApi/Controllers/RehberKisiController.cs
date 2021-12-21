using ContactReportApp.ContactApi.Entities;
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
        public ActionResult<List<KisiBilgileri>> KisilerGetir()
        {
            try
            {
                var kisiListesi = new List<KisiBilgileri>();

                var kisiler = _context.Kisiler.ToList();

                if (kisiler != null)
                {
                    foreach (var kisi in kisiler)
                    {
                        var kisiBilgileri = new KisiBilgileri() { Ad = kisi.Ad, Soyad = kisi.Soyad, Firma = kisi.Firma };


                        kisiBilgileri.Iletisimler = new List<Iletisim>();

                        var iletisimBilgileri = _context.IletisimBilgileri.Where(x => x.KisiId == kisi.Id).ToList();

                        foreach (var iletisimBilgi in iletisimBilgileri)
                        {
                            kisiBilgileri.Iletisimler.Add(new Iletisim() { BilgiTipi = iletisimBilgi.BilgiTipi.ToString(), BilgiIcerigi = iletisimBilgi.BilgiIcerigi });
                        }
                        kisiListesi.Add(kisiBilgileri);
                    }
                }
                return Ok(kisiListesi);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
