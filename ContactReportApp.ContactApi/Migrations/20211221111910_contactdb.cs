using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactReportApp.ContactApi.Migrations
{
    public partial class contactdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kisiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: true),
                    Soyad = table.Column<string>(type: "text", nullable: true),
                    Firma = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IletisimBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BilgiTipi = table.Column<int>(type: "integer", nullable: false),
                    BilgiIcerigi = table.Column<string>(type: "text", nullable: true),
                    KisiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IletisimBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IletisimBilgileri_Kisiler_KisiId",
                        column: x => x.KisiId,
                        principalTable: "Kisiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kisiler",
                columns: new[] { "Id", "Ad", "Firma", "Soyad" },
                values: new object[,]
                {
                    { 1, "Suphi", "Bir Firma", "Bayrak" },
                    { 2, "Ahmet", "Ikinci Firma", "Uslu" }
                });

            migrationBuilder.InsertData(
                table: "IletisimBilgileri",
                columns: new[] { "Id", "BilgiIcerigi", "BilgiTipi", "KisiId" },
                values: new object[,]
                {
                    { 1, "05346985877", 1, 1 },
                    { 2, "suphi.bayrak@yahoo.com", 2, 1 },
                    { 3, "Bursa", 3, 1 },
                    { 4, "05385988877", 1, 2 },
                    { 5, "Istanbul", 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IletisimBilgileri_KisiId",
                table: "IletisimBilgileri",
                column: "KisiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IletisimBilgileri");

            migrationBuilder.DropTable(
                name: "Kisiler");
        }
    }
}
