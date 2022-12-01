using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourTest.Data.Migrations
{
    public partial class addTablesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTour = table.Column<string>(maxLength: 100, nullable: false),
                    TourDate = table.Column<DateTime>(nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    TourId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paymentHistories_ClientTbl_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_paymentHistories_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    TourId = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => new { x.ClientId, x.TourId });
                    table.ForeignKey(
                        name: "FK_Reservations_ClientTbl_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_paymentHistories_ClientId",
                table: "paymentHistories",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentHistories_TourId",
                table: "paymentHistories",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TourId",
                table: "Reservations",
                column: "TourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentHistories");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
