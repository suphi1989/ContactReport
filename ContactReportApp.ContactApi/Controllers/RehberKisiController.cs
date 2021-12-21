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

        [Route("IletisimBilgileriGetir")]
        [HttpGet]
        public ActionResult<List<Iletisim>> IletisimBilgileriGetir(int KisiId)
        {
            try 
            {
                var iletisimler = new List<Iletisim>();

                var iletisimBilgileri = _context.IletisimBilgileri.Where(x => x.KisiId == KisiId).ToList();

                foreach (var iletisimBilgi in iletisimBilgileri)
                {
                    iletisimler.Add(new Iletisim() { BilgiTipi = iletisimBilgi.BilgiTipi.ToString(), BilgiIcerigi = iletisimBilgi.BilgiIcerigi });
                }

                return Ok(iletisimler);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisiOlustur")]
        [HttpPost]
        public ActionResult<int> KisiOlustur([FromBody] KisiModel model)
        {
            try
            {
                var kisi = new Kisi() { Ad = model.Ad, Firma = model.Firma, Soyad = model.Soyad };
              
                _context.Add(kisi);
                
                _context.SaveChanges();

                return Ok(kisi.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
      
        [Route("KisiKaldir")]
        [HttpPost]
        public ActionResult<string> KisiKaldir(int KisiId)
        {
            try
            {
                var iletisimler = _context.IletisimBilgileri.Where(x => x.KisiId == KisiId).ToList();
                if (iletisimler.Count > 0)
                {
                    _context.RemoveRange(iletisimler);
                    _context.SaveChanges();

                    var kisi = _context.Kisiler.Where(x => x.Id == KisiId).FirstOrDefault();
                    if (kisi != null)
                    {
                        _context.Remove(kisi);
                        _context.SaveChanges();
                    }
                    return Ok("Kişi bilgileri ile kişi iletişim bilgileri başarıyla kaldırılmıştır.");
                }
                else return NotFound("Kişi bilgileri bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisiIletisimBilgileriOlustur")]
        [HttpPost]
        public ActionResult<string> KisiIletisimBilgileriOlustur([FromBody] IletisimBilgileriModel model)
        {
            try
            {
                var kisi = _context.Kisiler.Where(x => x.Id == model.KisiId).FirstOrDefault();
                if (kisi != null)
                {
                    kisi.IletisimBilgileri = new List<IletisimBilgisi>();
                    kisi.IletisimBilgileri.Add(new IletisimBilgisi() { BilgiTipi = model.BilgiTipi, KisiId = model.KisiId, BilgiIcerigi = model.BilgiIcerigi });
                   
                    _context.SaveChanges();

                    return Ok("Kişi iletişim bilgileri başarıyla Oluşmuştur.");
                }
                else return NotFound("Kişi bulunamadı.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisiIletisimBilgileriKaldir")]
        [HttpPost]
        public ActionResult<string> KisiIletisimBilgileriKaldir(int Id)
        {
            try
            {
                var iletisimler = _context.IletisimBilgileri.Where(x => x.Id == Id).ToList();
                if (iletisimler.Count > 0)
                {
                    _context.RemoveRange(iletisimler);
                    _context.SaveChanges();
                    return Ok("Kişi iletişim bilgileri başarıyla kaldırılmıştır.");
                }
                else return NotFound("Kişi bilgileri bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisilerKonumaGoreGetir")]
        [HttpGet]
        public ActionResult<List<KisiBilgileri>> KisilerKonumaGoreGetir(string Konum)
        {
            try
            {
                var kisiListesi = new List<KisiBilgileri>();

                var kisiler = _context.Kisiler.ToList();

                if (kisiler != null)
                {
                    foreach (var kisi in kisiler)
                    {
                        var iletisimBilgileri = _context.IletisimBilgileri.Where(x => x.KisiId == kisi.Id && x.BilgiIcerigi.Contains(Konum) && x.BilgiTipi == BilgiTipi.Konum).ToList();
                        if (iletisimBilgileri.Count > 0)
                        {
                            var kisiBilgileri = new KisiBilgileri() { Ad = kisi.Ad, Soyad = kisi.Soyad, Firma = kisi.Firma };

                            kisiBilgileri.Iletisimler = new List<Iletisim>();

                            foreach (var iletisimBilgi in iletisimBilgileri)
                            {
                                kisiBilgileri.Iletisimler.Add(new Iletisim() { BilgiTipi = iletisimBilgi.BilgiTipi.ToString(), BilgiIcerigi = iletisimBilgi.BilgiIcerigi });
                            }
                            kisiListesi.Add(kisiBilgileri);
                        }
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
