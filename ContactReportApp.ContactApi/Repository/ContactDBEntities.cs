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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IletisimBilgisi>()
                .HasOne<Kisi>(s => s.Kisi)
                .WithMany(g => g.IletisimBilgileri)
                .HasForeignKey(s => s.KisiId);

            modelBuilder.Entity<Kisi>().HasData(
                new Kisi
                {
                    Id = 1,
                    Ad = "Suphi",
                    Soyad = "Bayrak",
                    Firma = "Bir Firma"
                },
                new Kisi
                {
                    Id = 2,
                    Ad = "Ahmet",
                    Soyad = "Uslu",
                    Firma = "Ikinci Firma"
                }
            );


            modelBuilder.Entity<IletisimBilgisi>().HasData(
                new IletisimBilgisi { Id = 1, KisiId = 1, BilgiTipi = BilgiTipi.TelefonNumarasi, BilgiIcerigi = "05346985877" },
                new IletisimBilgisi { Id = 2, KisiId = 1, BilgiTipi = BilgiTipi.Eposta, BilgiIcerigi = "suphi.bayrak@yahoo.com" },
                 new IletisimBilgisi { Id = 3, KisiId = 1, BilgiTipi = BilgiTipi.Konum, BilgiIcerigi = "Bursa" },
                  new IletisimBilgisi { Id = 4, KisiId = 2, BilgiTipi = BilgiTipi.TelefonNumarasi, BilgiIcerigi = "05385988877" },
                   new IletisimBilgisi { Id = 5, KisiId = 2, BilgiTipi = BilgiTipi.Konum, BilgiIcerigi = "Istanbul" }
                );

        }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<IletisimBilgisi> IletisimBilgileri { get; set; }
    }
}
