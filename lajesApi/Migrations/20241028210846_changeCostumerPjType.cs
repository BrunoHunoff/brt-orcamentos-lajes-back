using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class changeCostumerPjType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PJ",
                table: "costumers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PJ",
                table: "costumers",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }
    }
}
