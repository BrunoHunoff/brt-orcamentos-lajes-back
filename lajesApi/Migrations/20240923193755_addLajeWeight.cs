using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lajesApi.Migrations
{
    /// <inheritdoc />
    public partial class addLajeWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Lajes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Lajes");
        }
    }
}
