﻿using ContactReportApp.ContactApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp.ContactApi.Repository
{
    public class ContactDBContext : DbContext
    {
        public ContactDBContext(DbContextOptions<ContactDBContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    Firma = "Yazılım Firma1"
                },
                new Kisi
                {
                    Id = 2,
                    Ad = "Ahmet",
                    Soyad = "Uslu",
                    Firma = "Inşaat Firma2"
                },
                new Kisi
                {
                    Id = 3,
                    Ad = "Onur",
                    Soyad = "Yıldız",
                    Firma = "Yazılım Firma3"
                }
            );


            modelBuilder.Entity<IletisimBilgisi>().HasData(
                new IletisimBilgisi { Id = 1, KisiId = 1, BilgiTipi = BilgiTipi.TelefonNumarasi, BilgiIcerigi = "05346985877" },
                new IletisimBilgisi { Id = 2, KisiId = 1, BilgiTipi = BilgiTipi.Eposta, BilgiIcerigi = "suphi.bayrak@yahoo.com" },
                new IletisimBilgisi { Id = 3, KisiId = 1, BilgiTipi = BilgiTipi.Konum, BilgiIcerigi = "Bursa" },
                new IletisimBilgisi { Id = 4, KisiId = 2, BilgiTipi = BilgiTipi.TelefonNumarasi, BilgiIcerigi = "05385988877" },
                new IletisimBilgisi { Id = 5, KisiId = 2, BilgiTipi = BilgiTipi.Konum, BilgiIcerigi = "İstanbul" },
                new IletisimBilgisi { Id = 6, KisiId = 3, BilgiTipi = BilgiTipi.Konum, BilgiIcerigi = "Bursa" },
                new IletisimBilgisi { Id = 7, KisiId = 3, BilgiTipi = BilgiTipi.Eposta, BilgiIcerigi = "Onur@gmail.com" }
                );

        }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<IletisimBilgisi> IletisimBilgileri { get; set; }
    }
}
