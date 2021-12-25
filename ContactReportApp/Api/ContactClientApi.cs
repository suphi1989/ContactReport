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

        public void DeleteContact(int kisiId)
        {
            var request = GetRequest(Method.POST, "RehberKisi/KisiKaldir");
            request.AddBody(kisiId);
            _restApi.Execute(request);
        }

        public void DeleteContactDetay(int Id,int KisiId)
        {
            var request = GetRequest(Method.POST, string.Format("RehberKisi/KisiIletisimBilgileriKaldir/{0}", Id));
            request.AddBody(KisiId);
            var r = _restApi.Execute<string>(request);
        }

        public List<KisiIletisimBilgileriModel> GetContactDetay(int kisiId)
        {
            var request = GetRequest(Method.GET, "RehberKisi/IletisimBilgileriGetir");
            request.AddParameter("KisiId", kisiId);
            var result = _restApi.Execute<List<KisiIletisimBilgileriModel>>(request);
            return result.Data;

        }

        public int CreateContact(CreateContactViewModel model)
        {
            var request = GetRequest(Method.POST, "RehberKisi/KisiOlustur");
            request.AddBody(model);
            var response = _restApi.Execute<int>(request);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else return 0;
        }

        public bool CreateContactDetay(List<CreateContactDetayViewModel> model)
        {
            var request = GetRequest(Method.POST, "RehberKisi/KisiIletisimBilgileriOlustur");
            request.AddBody(model);
            var response = _restApi.Execute<string>(request);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK && response.Content == "\"Kişi iletişim bilgileri başarıyla Oluşmuştur.\"")
            {
                return true;
            }
            else return false;
        }
    }
}
