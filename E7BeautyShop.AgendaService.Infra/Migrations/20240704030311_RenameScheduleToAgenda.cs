using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E7BeautyShop.AgendaService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameScheduleToAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysRest_Schedules_ScheduleId",
                table: "DaysRest");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeHours_Schedules_ScheduleId",
                table: "OfficeHours");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "OfficeHours",
                newName: "AgendaId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficeHours_ScheduleId",
                table: "OfficeHours",
                newName: "IX_OfficeHours_AgendaId");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "DaysRest",
                newName: "AgendaId");

            migrationBuilder.RenameIndex(
                name: "IX_DaysRest_ScheduleId",
                table: "DaysRest",
                newName: "IX_DaysRest_AgendaId");

            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Professional_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weekday_StartAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekday_EndAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekend_StartAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekend_EndAt = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DaysRest_Agendas_AgendaId",
                table: "DaysRest",
                column: "AgendaId",
                principalTable: "Agendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeHours_Agendas_AgendaId",
                table: "OfficeHours",
                column: "AgendaId",
                principalTable: "Agendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysRest_Agendas_AgendaId",
                table: "DaysRest");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeHours_Agendas_AgendaId",
                table: "OfficeHours");

            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.RenameColumn(
                name: "AgendaId",
                table: "OfficeHours",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficeHours_AgendaId",
                table: "OfficeHours",
                newName: "IX_OfficeHours_ScheduleId");

            migrationBuilder.RenameColumn(
                name: "AgendaId",
                table: "DaysRest",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaysRest_AgendaId",
                table: "DaysRest",
                newName: "IX_DaysRest_ScheduleId");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Professional_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weekday_EndAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekday_StartAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekend_EndAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Weekend_StartAt = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DaysRest_Schedules_ScheduleId",
                table: "DaysRest",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeHours_Schedules_ScheduleId",
                table: "OfficeHours",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
