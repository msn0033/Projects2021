using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class Repository1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Finance_CurrentAsset_Inventory_Main_Repository_RepositoryId",
                table: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_Purchases_Finance_CurrentAsset_Inventory_Main_Repository_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Finance_Supplier_Purchases_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Finance_CurrentAsset_Inventory_Main_Repository",
                table: "Finance_CurrentAsset_Inventory_Main_Repository");

            migrationBuilder.DropColumn(
                name: "StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.RenameTable(
                name: "Finance_CurrentAsset_Inventory_Main_Repository",
                newName: "Repository");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repository",
                table: "Repository",
                column: "RepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Repository_RepositoryId",
                table: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem",
                column: "RepositoryId",
                principalTable: "Repository",
                principalColumn: "RepositoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Repository_RepositoryId",
                table: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repository",
                table: "Repository");

            migrationBuilder.RenameTable(
                name: "Repository",
                newName: "Finance_CurrentAsset_Inventory_Main_Repository");

            migrationBuilder.AddColumn<int>(
                name: "StoreTypeId",
                table: "Finance_Supplier_Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Finance_CurrentAsset_Inventory_Main_Repository",
                table: "Finance_CurrentAsset_Inventory_Main_Repository",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchases_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Finance_CurrentAsset_Inventory_Main_Repository_RepositoryId",
                table: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem",
                column: "RepositoryId",
                principalTable: "Finance_CurrentAsset_Inventory_Main_Repository",
                principalColumn: "RepositoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_Purchases_Finance_CurrentAsset_Inventory_Main_Repository_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId",
                principalTable: "Finance_CurrentAsset_Inventory_Main_Repository",
                principalColumn: "RepositoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
