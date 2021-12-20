using ContactReportApp.ContactApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Repository
{
    public class ContactDBEntities : DbContext
    {
        public ContactDBEntities(DbContextOptions<ContactDBEntities> options) : base(options) 
        {

        }
        public virtual DbSet<Kisi> Kisi { get; set; }
    }
}
