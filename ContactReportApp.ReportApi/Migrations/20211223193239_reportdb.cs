using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactReportApp.ReportApi.Migrations
{
    public partial class reportdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Raporlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RaporTarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DosyaYolu = table.Column<string>(type: "text", nullable: true),
                    RaporDurumu = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raporlar", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Raporlar",
                columns: new[] { "Id", "DosyaYolu", "RaporDurumu", "RaporTarihi" },
                values: new object[] { 1, "test yolu", 1, new DateTime(2021, 12, 23, 22, 32, 38, 884, DateTimeKind.Local).AddTicks(7795) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raporlar");
        }
    }
}
