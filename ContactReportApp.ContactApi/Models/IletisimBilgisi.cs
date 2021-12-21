using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Models
{
    public class IletisimBilgisi
    {
        public int Id { get; set; }
        public BilgiTipi BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public int KisiId { get; set; }
        public Kisi Kisi { get; set; }

    }
    public enum BilgiTipi
    {
        [Description("Telefon Numarası")]
        TelefonNumarasi = 1,
        [Description("E-mail Adresi")]
        Eposta = 2,
        [Description("Konum")]
        Konum = 3
    }

}
