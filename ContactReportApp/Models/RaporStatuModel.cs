using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class RaporStatuModel
    {
        public string RaporId { get; set; }
        public string DosyaYolu { get; set; }
        public RaporDurum RaporDurumu { get; set; }
    }
    public enum RaporDurum
    {
        [Description("Hazırlanıyor")]
        Hazirlaniyor = 0,
        [Description("Tamamlandı")]
        Tamamlandi = 1
    }
}
