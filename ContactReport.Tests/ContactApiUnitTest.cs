using ContactReportApp.ContactApi.Entities;
using ContactReportApp.ContactApi.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ContactReport.Tests
{
    public class ContactApiUnitTest
    {
        private RestClient _restApi;
        private string _apiUrl = "https://localhost:44305";
        [SetUp]
        public void Setup()
        {
            _restApi = new RestClient(_apiUrl);
        }

        [Test]
        public void KisilerGetirTest()
        {
            var request = new RestRequest("RehberKisi/KisilerGetir", Method.GET);
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
            var result = _restApi.Execute<List<KisiBilgileri>>(request);
            Assert.IsTrue(result != null && result.Data != null && result.Data.Count > 0);

            //RestSharp kullanmak istemezsek HttpWebRequest kullanabiliriz
            /*var request = (HttpWebRequest)WebRequest.Create("https://localhost:44305/RehberKisi/KisilerGetir");
            request.Method = "GET";
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }*/
        }
        [Test]
        public void IletisimBilgileriGetirTest()
        {
            var request = new RestRequest("RehberKisi/IletisimBilgileriGetir", Method.GET);
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.AddParameter("KisiId", 1);
            //request.AddParameter("KisiId", 2);
            request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
            var result = _restApi.Execute<List<Iletisim>>(request);
            Assert.IsTrue(result != null && result.Data != null && result.Data.Count > 0);
        }

        [Test]
        public void KisilerKonumaGoreGetirTest()
        {
            var request = new RestRequest("RehberKisi/KisilerKonumaGoreGetir", Method.GET);
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.AddParameter("Konum", "Istanbul");
            request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
            var result = _restApi.Execute<List<RaporIstatistikModel>>(request);
            Assert.IsTrue(result != null && result.Data != null && result.Data.Count > 0);
        }

        [Test]
        public void KisiVeIletisimOlustur()
        {
            var request = new RestRequest("RehberKisi/KisiOlustur", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.OnBeforeDeserialization = x =>
            {
                x.ContentType = "application/json";
            };
            var kisi = new KisiModel()
            {
                Ad = "Mehmet",
                Soyad = "Yýlmaz",
                Firma = "111"
            };
            request.AddBody(kisi);
            
            var result = _restApi.Execute<int>(request);
            
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);

            if (result != null && result.Data > 0)
            {
                var request2 = new RestRequest("RehberKisi/KisiIletisimBilgileriOlustur", Method.POST);
                request2.RequestFormat = DataFormat.Json;
                request2.OnBeforeDeserialization = x =>
                {
                    x.ContentType = "application/json";
                };
                var iletisimList = new List<IletisimBilgileriModel>() 
                { 
                    new IletisimBilgileriModel()
                    {
                        KisiId = result.Data,
                        BilgiIcerigi = "05356828282",
                        BilgiTipi = BilgiTipi.TelefonNumarasi
                    },
                    new IletisimBilgileriModel() 
                    {
                        KisiId = result.Data,
                        BilgiIcerigi = "Bursa",
                        BilgiTipi = BilgiTipi.Konum
                    }
                };

                request2.AddBody(iletisimList);
                
                var result2 = _restApi.Execute<int>(request2);
                Assert.IsTrue(result2.StatusCode == HttpStatusCode.OK);
            }
        }
    }
}