using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class CreateContactDetayViewModel
    {
        public BilgiTipi BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public int KisiId { get; set; }
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
