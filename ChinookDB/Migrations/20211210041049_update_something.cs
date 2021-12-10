using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChinookDB.Migrations
{
    public partial class update_something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_Customer_Id",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Customer_Id",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Id",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "Customer_Id",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Customer_Id",
                table: "Invoices",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_Customer_Id",
                table: "Invoices",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
