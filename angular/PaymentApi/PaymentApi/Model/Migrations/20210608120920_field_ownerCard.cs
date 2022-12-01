using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentApi.model.Migrations
{
    public partial class field_ownerCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardOnerName",
                table: "PaymentDetails");

            migrationBuilder.AddColumn<string>(
                name: "CardOwnerName",
                table: "PaymentDetails",
                type: "nvarchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardOwnerName",
                table: "PaymentDetails");

            migrationBuilder.AddColumn<string>(
                name: "CardOnerName",
                table: "PaymentDetails",
                type: "nvarchar(100)",
                nullable: true);
        }
    }
}
