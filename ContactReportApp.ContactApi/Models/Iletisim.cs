﻿using ContactReportApp.ContactApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Models
{
    public class Iletisim
    {
        public int Id { get; set; }
        public string BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
    }
}
