using ContactReportApp.ContactApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Models
{
    public class IletisimBilgileriModel
    {
        public BilgiTipi BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public int KisiId { get; set; }
    }
}
