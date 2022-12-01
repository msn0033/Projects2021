using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class editcheckTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Finance_CurrentAsset_Checks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JournalId",
                table: "Finance_CurrentAsset_Checks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
