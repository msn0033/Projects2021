using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class seedingAccountChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChart",
                columns: new[] { "AccNum", "AccTypeId", "AccountName", "AccountNameAr", "AccountNature", "Balance", "BranchId", "CurrencyId", "IsActive", "IsParent", "ParentAcNum", "StartingBalance" },
                values: new object[] { "1230000001", 6, "Clients Account", " حساب العملاء", 0, 0m, 1, 1, true, false, "1230000000", 0m });

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChart",
                columns: new[] { "AccNum", "AccTypeId", "AccountName", "AccountNameAr", "AccountNature", "Balance", "BranchId", "CurrencyId", "IsActive", "IsParent", "ParentAcNum", "StartingBalance" },
                values: new object[] { "2170000001", 13, "Suppliers Account ", "حساب  للموردين ", 1, 0m, 1, 1, true, false, "2170000000", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1230000001");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000001");
        }
    }
}
