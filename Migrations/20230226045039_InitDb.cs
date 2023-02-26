using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.API.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificateCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertPublicKeyRaw = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CertPrivateKeyRaw = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CertRawData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateCategories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateCategories");
        }
    }
}
