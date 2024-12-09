using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Servisler_ServislerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServislerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServislerId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Servisler_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Servisler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Servisler_ServiceId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "ServislerId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServislerId",
                table: "Appointments",
                column: "ServislerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Servisler_ServislerId",
                table: "Appointments",
                column: "ServislerId",
                principalTable: "Servisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
