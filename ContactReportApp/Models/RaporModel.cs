using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class RaporModel
    {
        public int Id { get; set; }
        public DateTime RaporTarihi { get; set; }
        public string DosyaYolu { get; set; }
        public RaporDurum RaporDurumu { get; set; }
    }
}
