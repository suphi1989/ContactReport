using Confluent.Kafka;
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
        private ProducerConfig _config;

        public RehberKisiController(ILogger<RehberKisiController> logger, ContactDBContext context, ProducerConfig config)
        {
            _logger = logger;
            _context = context;
            _config = config;
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
                        var kisiBilgileri = new KisiBilgileri() { Id = kisi.Id, Ad = kisi.Ad, Soyad = kisi.Soyad, Firma = kisi.Firma };


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
                    iletisimler.Add(new Iletisim() { Id = iletisimBilgi.Id, BilgiTipi = iletisimBilgi.BilgiTipi.ToString(), BilgiIcerigi = iletisimBilgi.BilgiIcerigi });
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
        public ActionResult<string> KisiKaldir([FromBody] int KisiId)
        {
            try
            {
                var iletisimler = _context.IletisimBilgileri.Where(x => x.KisiId == KisiId).ToList();
                if (iletisimler.Count > 0)
                {
                    _context.RemoveRange(iletisimler);
                    _context.SaveChanges();
                }

                var kisi = _context.Kisiler.Where(x => x.Id == KisiId).FirstOrDefault();
                if (kisi != null)
                {
                    _context.Remove(kisi);
                    _context.SaveChanges();
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
        public ActionResult<string> KisiIletisimBilgileriOlustur([FromBody] List<IletisimBilgileriModel> iletisimBilgileriList)
        {
            try
            {
                var kisi = _context.Kisiler.Where(x => x.Id == iletisimBilgileriList.FirstOrDefault().KisiId).FirstOrDefault();
                if (kisi != null)
                {
                    kisi.IletisimBilgileri = new List<IletisimBilgisi>();
                    foreach (var item in iletisimBilgileriList)
                    {
                        kisi.IletisimBilgileri.Add(new IletisimBilgisi() { BilgiTipi = item.BilgiTipi, KisiId = item.KisiId, BilgiIcerigi = item.BilgiIcerigi });
                    }
                    _context.SaveChanges();
                }

                return Ok("Kişi iletişim bilgileri başarıyla Oluşmuştur.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisiIletisimBilgileriKaldir/{Id:int}")]
        [HttpPost]
        public ActionResult<string> KisiIletisimBilgileriKaldir([FromRoute] int Id,[FromBody] int KisiId)
        {
            try
            {
                var iletisimler = _context.IletisimBilgileri.Where(x => x.KisiId == KisiId && x.Id == Id).ToList();
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
        public ActionResult<RaporIstatistikModel> KisilerKonumaGoreGetir(string Konum)
        {
            try
            {
                var result = new RaporIstatistikModel();
                result.Detaylar = new List<Detay>();

                int kisiSayisi = 0;
                int telefonKisiSayisi = 0;
               
                var kisiler = _context.Kisiler.ToList();

                foreach (var kisi in kisiler)
                {
                    var iletisimListesi = _context.IletisimBilgileri.Where(x => x.KisiId == kisi.Id && x.BilgiTipi == BilgiTipi.Konum && x.BilgiIcerigi.Contains(Konum)).ToList();
                    if (iletisimListesi.Count > 0)
                    {
                        kisiSayisi++;

                        var iletisimListesi2 = _context.IletisimBilgileri.Where(x => x.KisiId == kisi.Id && x.BilgiTipi == BilgiTipi.TelefonNumarasi && !string.IsNullOrEmpty(x.BilgiIcerigi)).ToList();
                        if (iletisimListesi2.Count > 0)
                        {
                            telefonKisiSayisi++;
                        }
                        result.Detaylar.Add(new Detay
                        {
                            Ad = kisi.Ad,
                            Soyad = kisi.Soyad,
                            KonumBilgisi = string.Join("-", iletisimListesi.Select(x => x.BilgiIcerigi).ToList()),
                            Telefon = string.Join("-", iletisimListesi2.Select(x => x.BilgiIcerigi).ToList()),
                        });

                    }
                }

                if (kisiSayisi > 0)
                {
                    result.KayitliTelefonNumarasiSayisi = telefonKisiSayisi;
                    result.KayitliKisiSayisi = kisiSayisi;
                    return Ok(result);
                }
                else
                    return NotFound("Kişiler bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("KisilerKonumaGoreRaporuOlustur")]
        [HttpGet]
        //bu endpoint report api den çağırıyor raporu kafka ya göndermek için.
        public async Task<ActionResult> KisilerKonumaGoreRaporuOlustur(string Konum,string RaporId)
        {
            try
            {
                var result = KisilerKonumaGoreGetir(Konum).Result;

                //Kişiler bulunamadı

                //{ "Value":"Kişiler bulunamadı.","Formatters":[],"ContentTypes":[],"DeclaredType":null,"StatusCode":404}

                //Başarılı

                /*  35***{"Value":{"Detaylar":[{"Ad":"Suphi","Soyad":"Bayrak","KonumBilgisi":"Bursa","Telefon":"05346985877"},{"Ad":"Mehmet","Soyad":"Yılmaz","KonumBilgisi":"Bursa","Telefon":"05356828282"}],"KayitliKisiSayisi":2,"KayitliTelefonNumarasiSayisi":2},"Formatters":[],"ContentTypes":[],"DeclaredType":null,"StatusCode":200} */

                var jsonData = JsonConvert.SerializeObject(result);

                _logger.LogInformation("Sender:" + RaporId + "***" + jsonData);
                //json data Kafka servis'ye göndereceğiz

                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    await producer.ProduceAsync("report-topic", new Message<Null, string> { Value = RaporId + "***" + jsonData });
                    producer.Flush(TimeSpan.FromSeconds(10));
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
