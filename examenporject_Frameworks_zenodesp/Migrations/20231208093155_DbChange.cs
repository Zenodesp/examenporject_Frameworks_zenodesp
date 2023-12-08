using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace examenporject_Frameworks_zenodesp.Migrations
{
    /// <inheritdoc />
    public partial class DbChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_EmployeeUser",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_EmployeeUser",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "EmployeeUser",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_EmployeeId",
                table: "Appointment",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_EmployeeId",
                table: "Appointment",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_EmployeeId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_EmployeeId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeUser",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_EmployeeUser",
                table: "Appointment",
                column: "EmployeeUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_EmployeeUser",
                table: "Appointment",
                column: "EmployeeUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
