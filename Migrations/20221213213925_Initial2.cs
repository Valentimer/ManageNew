using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerFamily.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_SpendingCategories_DepartmentId",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Positions",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Positions",
                newName: "SpendingCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                newName: "IX_Positions_SpendingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_SpendingCategories_SpendingCategoryId",
                table: "Positions",
                column: "SpendingCategoryId",
                principalTable: "SpendingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_SpendingCategories_SpendingCategoryId",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "SpendingCategoryId",
                table: "Positions",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Positions",
                newName: "Salary");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_SpendingCategoryId",
                table: "Positions",
                newName: "IX_Positions_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_SpendingCategories_DepartmentId",
                table: "Positions",
                column: "DepartmentId",
                principalTable: "SpendingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
