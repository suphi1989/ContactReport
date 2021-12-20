﻿// <auto-generated />
using ContactReportApp.ContactApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactReportApp.ContactApi.Migrations
{
    [DbContext(typeof(ContactDBEntities))]
    partial class ContactDBEntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ContactReportApp.ContactApi.Models.Kisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Ad")
                        .HasColumnType("text");

                    b.Property<string>("Firma")
                        .HasColumnType("text");

                    b.Property<string>("Soyad")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Kisi");
                });
#pragma warning restore 612, 618
        }
    }
}
