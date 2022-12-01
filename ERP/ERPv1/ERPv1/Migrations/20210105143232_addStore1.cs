using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class addStore1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypes_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropColumn(
                name: "StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.AddColumn<int>(
                name: "StoreTypeId",
                table: "Finance_Supplier_Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchases_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_Purchases_StoreTypes_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId",
                principalTable: "StoreTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_Purchases_StoreTypes_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Finance_Supplier_Purchases_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropColumn(
                name: "StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.AddColumn<int>(
                name: "StoreTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
