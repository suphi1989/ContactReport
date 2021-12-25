using ContactReportApp.ReportApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ReportApi.Models
{
    public class RaporStatuModel
    {
        public string RaporId { get; set; }
        public string DosyaYolu { get; set; }
        public RaporDurum RaporDurumu { get; set; }
    }
}
