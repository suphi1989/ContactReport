using ContactReportApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactReportApp.Api
{
    public class ReportClientApi
    {
        private RestClient _restApi;
        private string _apiUrl = "https://localhost:44333";
        public ReportClientApi()
        {
            _restApi = new RestClient(_apiUrl);
        }
        public RestRequest GetRequest(Method metot, string url)
        {
            var request = new RestRequest(url, metot);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("admin" + ":" + "123"))));
            request.OnBeforeDeserialization = x => { x.ContentType = "application/json"; };
            return request;
        }
        public string ReportCreate(string Konum)
        {
            var request = GetRequest(Method.POST, "Rapor/RaporOlustur");
            request.AddBody(Konum);
            var result = _restApi.Execute(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return result.Content+ ". Raporu Oluşturuluyor, lütfen Bekleyiniz.";
            else return "Raporu oluştururken bir hata oluştu";
        }
        public List<RaporModel> GetReportList()
        {
            var request = GetRequest(Method.GET, "Rapor/RaporlarGetir");
            var result = _restApi.Execute<List<RaporModel>>(request);
            return result.Data;
        }
        public void ReportDelete(int raporId)
        {
            var request = GetRequest(Method.POST, "Rapor/RaporKaldir");
            request.AddBody(raporId);
            _restApi.Execute(request);
        }
    }
}
