using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class ExcelRaporModel
    {
        public Value Value { get; set; }
        public List<string> Formatters { get; set; }
        public List<string> ContentTypes { get; set; }
        public string DeclaredType { get; set; }
        public int StatusCode { get; set; }
    }
    public class Value
    {
        public List<Detaylar> Detaylar { get; set; }
        public int KayitliKisiSayisi { get; set; }
        public int KayitliTelefonNumarasiSayisi { get; set; }
    }
    public class Detaylar
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KonumBilgisi { get; set; }
        public string Telefon { get; set; }
    }
}
