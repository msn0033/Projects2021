using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class Repository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_Purchases_StoreTypes_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropTable(
                name: "StoreTypes");

            migrationBuilder.DropColumn(
                name: "StoreLocation",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_Repository",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_Repository", x => x.RepositoryId);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false),
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem", x => new { x.RepositoryId, x.StoreItemId });
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Finance_CurrentAsset_Inventory_Main_Repository_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_Repository",
                        principalColumn: "RepositoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_Inventory_Main_Repository",
                columns: new[] { "RepositoryId", "RepositoryName" },
                values: new object[] { 1, "MainRepository" });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem",
                column: "StoreItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_Purchases_Finance_CurrentAsset_Inventory_Main_Repository_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId",
                principalTable: "Finance_CurrentAsset_Inventory_Main_Repository",
                principalColumn: "RepositoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_Purchases_Finance_CurrentAsset_Inventory_Main_Repository_StoreTypeId",
                table: "Finance_Supplier_Purchases");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_Repository");

            migrationBuilder.AddColumn<string>(
                name: "StoreLocation",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_Purchases_StoreTypes_StoreTypeId",
                table: "Finance_Supplier_Purchases",
                column: "StoreTypeId",
                principalTable: "StoreTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
