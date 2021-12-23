using ContactReportApp.ReportApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ReportApi.Repository
{
    public class ReportDBContext : DbContext
    {
        public ReportDBContext(DbContextOptions<ReportDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rapor>().HasData(
               new Rapor
               {
                   Id = 1,
                   RaporTarihi = DateTime.Now,
                   DosyaYolu = "test yolu",
                   RaporDurumu = RaporDurum.Tamamlandi
               }
           );
        }
        public DbSet<Rapor> Raporlar { get; set; }
    }
}
