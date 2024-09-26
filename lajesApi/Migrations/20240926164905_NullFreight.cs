using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class NullFreight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets");

            migrationBuilder.AlterColumn<int>(
                name: "FreightId",
                table: "budgets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets",
                column: "FreightId",
                principalTable: "freights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets");

            migrationBuilder.AlterColumn<int>(
                name: "FreightId",
                table: "budgets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets",
                column: "FreightId",
                principalTable: "freights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
