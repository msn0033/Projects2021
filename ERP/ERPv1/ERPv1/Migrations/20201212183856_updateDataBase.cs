using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class updateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JournalId",
                table: "Finance_CurrentAsset_Checks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_CheckLocation",
                columns: new[] { "Id", "CheckLocationAR", "CheckLocationEN", "IsDefault" },
                values: new object[] { 5, "تم تحصيل نقداً وارجاع الشيك للعميل", "Collect Cash and Return to Client", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Finance_CurrentAsset_CheckLocation",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Finance_CurrentAsset_Checks");
        }
    }
}
