using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementApp.Migrations
{
    public partial class Added_Cardinality_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations",
                column: "university_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_bookings_employee_id",
                table: "tb_m_bookings",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_bookings_room_id",
                table: "tb_m_bookings",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_roles_account guid",
                table: "tb_m_accounts_roles",
                column: "account guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_roles_role guid",
                table: "tb_m_accounts_roles",
                column: "role guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_guid",
                table: "tb_m_accounts",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_roles_tb_m_accounts_account guid",
                table: "tb_m_accounts_roles",
                column: "account guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_roles_tb_m_roles_role guid",
                table: "tb_m_accounts_roles",
                column: "role guid",
                principalTable: "tb_m_roles",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_bookings_tb_m_employees_employee_id",
                table: "tb_m_bookings",
                column: "employee_id",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_bookings_tb_m_rooms_room_id",
                table: "tb_m_bookings",
                column: "room_id",
                principalTable: "tb_m_rooms",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations",
                column: "university_guid",
                principalTable: "tb_m_universities",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_guid",
                table: "tb_m_accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_roles_tb_m_accounts_account guid",
                table: "tb_m_accounts_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_roles_tb_m_roles_role guid",
                table: "tb_m_accounts_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_bookings_tb_m_employees_employee_id",
                table: "tb_m_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_bookings_tb_m_rooms_room_id",
                table: "tb_m_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_bookings_employee_id",
                table: "tb_m_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_bookings_room_id",
                table: "tb_m_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_accounts_roles_account guid",
                table: "tb_m_accounts_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_accounts_roles_role guid",
                table: "tb_m_accounts_roles");
        }
    }
}
