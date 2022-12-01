using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentApi.model.Migrations
{
    public partial class name_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityCode",
                table: "PaymentDetails",
                newName: "securityCode");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "PaymentDetails",
                newName: "expirationDate");

            migrationBuilder.RenameColumn(
                name: "CardOwnerName",
                table: "PaymentDetails",
                newName: "cardOwnerName");

            migrationBuilder.RenameColumn(
                name: "CardNumber",
                table: "PaymentDetails",
                newName: "cardNumber");

            migrationBuilder.RenameColumn(
                name: "PaymentDetalisId",
                table: "PaymentDetails",
                newName: "paymentDetalisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "securityCode",
                table: "PaymentDetails",
                newName: "SecurityCode");

            migrationBuilder.RenameColumn(
                name: "expirationDate",
                table: "PaymentDetails",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "cardOwnerName",
                table: "PaymentDetails",
                newName: "CardOwnerName");

            migrationBuilder.RenameColumn(
                name: "cardNumber",
                table: "PaymentDetails",
                newName: "CardNumber");

            migrationBuilder.RenameColumn(
                name: "paymentDetalisId",
                table: "PaymentDetails",
                newName: "PaymentDetalisId");
        }
    }
}
