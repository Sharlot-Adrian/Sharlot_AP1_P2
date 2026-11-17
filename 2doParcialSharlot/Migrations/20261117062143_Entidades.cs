using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2doParcialSharlot.Migrations
{
    /// <inheritdoc />
    public partial class Entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Model1_VentaId",
                table: "VentasDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model1",
                table: "Model1");

            migrationBuilder.RenameTable(
                name: "Model1",
                newName: "Ventas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ventas",
                table: "Ventas",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Ventas_VentaId",
                table: "VentasDetalle",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Ventas_VentaId",
                table: "VentasDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ventas",
                table: "Ventas");

            migrationBuilder.RenameTable(
                name: "Ventas",
                newName: "Model1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model1",
                table: "Model1",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Model1_VentaId",
                table: "VentasDetalle",
                column: "VentaId",
                principalTable: "Model1",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
