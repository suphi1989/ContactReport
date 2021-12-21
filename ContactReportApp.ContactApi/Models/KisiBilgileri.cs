using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Models
{
    public class KisiBilgileri
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public List<Iletisim> Iletisimler { get; set; }
}
}
