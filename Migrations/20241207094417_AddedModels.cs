using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personaller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzmanlık = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personaller_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servisler_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal_Calisma_Zamanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal_Calisma_Zamanlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personal_Calisma_Zamanlari_Personaller_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personaller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServislerId = table.Column<int>(type: "int", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Onay = table.Column<bool>(type: "bit", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Personaller_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personaller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Servisler_ServislerId",
                        column: x => x.ServislerId,
                        principalTable: "Servisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal_servisleri",
                columns: table => new
                {
                    PersonalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServislerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal_servisleri", x => x.PersonalID);
                    table.ForeignKey(
                        name: "FK_Personal_servisleri_Personaller_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personaller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_servisleri_Servisler_ServislerId",
                        column: x => x.ServislerId,
                        principalTable: "Servisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PersonalId",
                table: "Appointments",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServislerId",
                table: "Appointments",
                column: "ServislerId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Calisma_Zamanlari_PersonalId",
                table: "Personal_Calisma_Zamanlari",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_servisleri_PersonalId",
                table: "Personal_servisleri",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_servisleri_ServislerId",
                table: "Personal_servisleri",
                column: "ServislerId");

            migrationBuilder.CreateIndex(
                name: "IX_Personaller_SalonId",
                table: "Personaller",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Servisler_SalonId",
                table: "Servisler",
                column: "SalonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Personal_Calisma_Zamanlari");

            migrationBuilder.DropTable(
                name: "Personal_servisleri");

            migrationBuilder.DropTable(
                name: "Personaller");

            migrationBuilder.DropTable(
                name: "Servisler");

            migrationBuilder.DropTable(
                name: "Salonlar");
        }
    }
}
