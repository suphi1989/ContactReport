using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Models
{
    public class RaporIstatistikModel
    {
        public List<Detay> Detaylar { get; set; }
        public int KayitliKisiSayisi { get; set; }
        public int KayitliTelefonNumarasiSayisi { get; set; }
    }
    public class Detay
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KonumBilgisi { get; set; }
        public string Telefon { get; set; }
    }
}
