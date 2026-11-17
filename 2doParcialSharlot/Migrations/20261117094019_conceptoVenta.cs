using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2doParcialSharlot.Migrations
{
    /// <inheritdoc />
    public partial class conceptoVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Concepto",
                table: "Ventas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concepto",
                table: "Ventas");
        }
    }
}
