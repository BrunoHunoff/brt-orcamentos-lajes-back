using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class budgetFreightPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FreightPrice",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightPrice",
                table: "budgets");
        }
    }
}
