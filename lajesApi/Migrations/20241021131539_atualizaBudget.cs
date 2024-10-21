using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class atualizaBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets");

            migrationBuilder.DropTable(
                name: "freights");

            migrationBuilder.DropIndex(
                name: "IX_budgets_FreightId",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "FreightId",
                table: "budgets");


            migrationBuilder.AlterColumn<double>(
                name: "FreightPrice",
                table: "budgets",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FreightType",
                table: "budgets",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "FreightWeight",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "FreightType",
                table: "budgets");

            migrationBuilder.DropColumn(
                name: "FreightWeight",
                table: "budgets");

            migrationBuilder.AlterColumn<double>(
                name: "FreightPrice",
                table: "budgets",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreightId",
                table: "budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "freights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freights", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_budgets_FreightId",
                table: "budgets",
                column: "FreightId");

            migrationBuilder.AddForeignKey(
                name: "FK_budgets_freights_FreightId",
                table: "budgets",
                column: "FreightId",
                principalTable: "freights",
                principalColumn: "Id");
        }
    }
}
