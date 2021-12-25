using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class ContactViewModel
    {
        public List<KisiBilgileriModel> Kisiler { get; set; }
        public List<KisiIletisimBilgileriModel> IletisimBilgileri { get; set; }
    }
}
