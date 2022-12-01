using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Migrations
{
    public partial class ee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_TransId",
                table: "Finance_Supplier_SupplierTransaction");

            migrationBuilder.RenameColumn(
                name: "TransId",
                table: "Finance_Supplier_SupplierTransaction",
                newName: "JournalId");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_TransId",
                table: "Finance_Supplier_SupplierTransaction",
                newName: "IX_Finance_Supplier_SupplierTransaction_JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_JournalId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "JournalId",
                principalTable: "Finance_GL_Journal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_JournalId",
                table: "Finance_Supplier_SupplierTransaction");

            migrationBuilder.RenameColumn(
                name: "JournalId",
                table: "Finance_Supplier_SupplierTransaction",
                newName: "TransId");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_JournalId",
                table: "Finance_Supplier_SupplierTransaction",
                newName: "IX_Finance_Supplier_SupplierTransaction_TransId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_TransId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "TransId",
                principalTable: "Finance_GL_Journal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
