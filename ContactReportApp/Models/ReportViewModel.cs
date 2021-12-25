using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.Models
{
    public class ReportViewModel
    {
        public List<RaporModel> raporListesi { get; set; }
        public string Arama { get; set; }
    }
}
