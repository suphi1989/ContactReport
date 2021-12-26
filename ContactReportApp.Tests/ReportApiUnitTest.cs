using ContactReportApp.ReportApi.Entities;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactReport.Tests
{
    public class ReportApiUnitTest
    {
        private RestClient _restApi;
        private string _apiUrl = "https://localhost:44333";

        [SetUp]
        public void Setup()
        {
            _restApi = new RestClient(_apiUrl);
        }

        [Test]
        public void ReporlarGetirTest()
        {
            var request = new RestRequest("Rapor/RaporlarGetir", Method.GET);
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
            var result = _restApi.Execute<List<Rapor>>(request);
            Assert.IsTrue(result != null && result.Data != null && result.Data.Count > 0);
        }
    }
}
