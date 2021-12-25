using ContactReportApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactReportApp.Api
{
    public class ContactClientApi
    {
        private RestClient _restApi;
        private string _apiUrl = "https://localhost:44305";
        public ContactClientApi()
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
        public List<KisiBilgileriModel> GetContactList()
        {
            var request = GetRequest(Method.GET, "RehberKisi/KisilerGetir");
            var result = _restApi.Execute<List<KisiBilgileriModel>>(request);
            return result.Data;
        }
    }
}
