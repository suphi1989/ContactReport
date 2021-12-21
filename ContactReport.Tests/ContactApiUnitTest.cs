using NUnit.Framework;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ContactReport.Tests
{
    public class ContactApiUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void KisilerGetirTest()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://localhost:44305/RehberKisi/KisilerGetir");
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
            }

            Assert.IsTrue(!string.IsNullOrEmpty(content));
        }
    }
}