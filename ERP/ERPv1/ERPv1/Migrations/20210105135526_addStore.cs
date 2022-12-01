using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class addStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameStore = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "StoreTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypes_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "StoreTypeId",
                principalTable: "StoreTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypes_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropTable(
                name: "StoreTypes");

            migrationBuilder.DropIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropColumn(
                name: "StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");
        }
    }
}
