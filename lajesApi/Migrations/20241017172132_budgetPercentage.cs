using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class budgetPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budgetSummaries");

            migrationBuilder.AddColumn<double>(
                name: "Administration",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Extra",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Profit",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Taxes",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administration",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "Extra",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "Taxes",
                table: "budgets");

            migrationBuilder.CreateTable(
                name: "budgetSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Administration = table.Column<double>(type: "double", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    Contribution = table.Column<double>(type: "double", nullable: false),
                    Extra = table.Column<double>(type: "double", nullable: false),
                    FreightWeight = table.Column<double>(type: "double", nullable: false),
                    Taxes = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budgetSummaries", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
