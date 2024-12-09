using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class DataAnnotationsAndSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Servisler_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_Calisma_Zamanlari_Personaller_PersonalId",
                table: "Personal_Calisma_Zamanlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_servisleri_Personaller_PersonalId",
                table: "Personal_servisleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_servisleri_Servisler_ServislerId",
                table: "Personal_servisleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personal_servisleri",
                table: "Personal_servisleri");

            migrationBuilder.DropIndex(
                name: "IX_Personal_servisleri_PersonalId",
                table: "Personal_servisleri");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Uzmanlık",
                table: "Personaller");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Personal_Calisma_Zamanlari");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Servisler",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Salonlar",
                newName: "SalonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Personaller",
                newName: "PersonalID");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Personal_servisleri",
                newName: "PersonalID");

            migrationBuilder.RenameColumn(
                name: "ServislerId",
                table: "Personal_servisleri",
                newName: "ServislerServiceId");

            migrationBuilder.RenameColumn(
                name: "PersonalID",
                table: "Personal_servisleri",
                newName: "PersonalID1");

            migrationBuilder.RenameIndex(
                name: "IX_Personal_servisleri_ServislerId",
                table: "Personal_servisleri",
                newName: "IX_Personal_servisleri_ServislerServiceId");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Personal_Calisma_Zamanlari",
                newName: "PersonalID");

            migrationBuilder.RenameIndex(
                name: "IX_Personal_Calisma_Zamanlari_PersonalId",
                table: "Personal_Calisma_Zamanlari",
                newName: "IX_Personal_Calisma_Zamanlari_PersonalID");

            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uzmanlik",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalID",
                table: "Personal_servisleri",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalID1",
                table: "Personal_servisleri",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ServislerServiceId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personal_servisleri",
                table: "Personal_servisleri",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_servisleri_PersonalID1",
                table: "Personal_servisleri",
                column: "PersonalID1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServislerServiceId",
                table: "Appointments",
                column: "ServislerServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Servisler_ServislerServiceId",
                table: "Appointments",
                column: "ServislerServiceId",
                principalTable: "Servisler",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_Calisma_Zamanlari_Personaller_PersonalID",
                table: "Personal_Calisma_Zamanlari",
                column: "PersonalID",
                principalTable: "Personaller",
                principalColumn: "PersonalID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_servisleri_Personaller_PersonalID1",
                table: "Personal_servisleri",
                column: "PersonalID1",
                principalTable: "Personaller",
                principalColumn: "PersonalID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_servisleri_Servisler_ServislerServiceId",
                table: "Personal_servisleri",
                column: "ServislerServiceId",
                principalTable: "Servisler",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Servisler_ServislerServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_Calisma_Zamanlari_Personaller_PersonalID",
                table: "Personal_Calisma_Zamanlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_servisleri_Personaller_PersonalID1",
                table: "Personal_servisleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Personal_servisleri_Servisler_ServislerServiceId",
                table: "Personal_servisleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personal_servisleri",
                table: "Personal_servisleri");

            migrationBuilder.DropIndex(
                name: "IX_Personal_servisleri_PersonalID1",
                table: "Personal_servisleri");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServislerServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Uzmanlik",
                table: "Personaller");

            migrationBuilder.DropColumn(
                name: "ServislerServiceId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Servisler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SalonId",
                table: "Salonlar",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonalID",
                table: "Personaller",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonalID",
                table: "Personal_servisleri",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "ServislerServiceId",
                table: "Personal_servisleri",
                newName: "ServislerId");

            migrationBuilder.RenameColumn(
                name: "PersonalID1",
                table: "Personal_servisleri",
                newName: "PersonalID");

            migrationBuilder.RenameIndex(
                name: "IX_Personal_servisleri_ServislerServiceId",
                table: "Personal_servisleri",
                newName: "IX_Personal_servisleri_ServislerId");

            migrationBuilder.RenameColumn(
                name: "PersonalID",
                table: "Personal_Calisma_Zamanlari",
                newName: "PersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Personal_Calisma_Zamanlari_PersonalID",
                table: "Personal_Calisma_Zamanlari",
                newName: "IX_Personal_Calisma_Zamanlari_PersonalId");

            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Uzmanlık",
                table: "Personaller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonalId",
                table: "Personal_servisleri",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalID",
                table: "Personal_servisleri",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Personal_Calisma_Zamanlari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personal_servisleri",
                table: "Personal_servisleri",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_servisleri_PersonalId",
                table: "Personal_servisleri",
                column: "PersonalId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_Calisma_Zamanlari_Personaller_PersonalId",
                table: "Personal_Calisma_Zamanlari",
                column: "PersonalId",
                principalTable: "Personaller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_servisleri_Personaller_PersonalId",
                table: "Personal_servisleri",
                column: "PersonalId",
                principalTable: "Personaller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_servisleri_Servisler_ServislerId",
                table: "Personal_servisleri",
                column: "ServislerId",
                principalTable: "Servisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
