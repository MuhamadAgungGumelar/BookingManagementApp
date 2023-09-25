using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    expired_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountguid = table.Column<Guid>(name: "account guid", type: "uniqueidentifier", nullable: false),
                    roleguid = table.Column<Guid>(name: "role guid", type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts_roles", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_bookings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_bookings", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_educations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    major = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    degree = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    gpa = table.Column<float>(type: "real", nullable: false),
                    university_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_educations", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employees",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nik = table.Column<string>(type: "nchar(6)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employees", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_rooms",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_rooms", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_universities",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_universities", x => x.guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_nik_email_phone_number",
                table: "tb_m_employees",
                columns: new[] { "nik", "email", "phone_number" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_accounts_roles");

            migrationBuilder.DropTable(
                name: "tb_m_bookings");

            migrationBuilder.DropTable(
                name: "tb_m_educations");

            migrationBuilder.DropTable(
                name: "tb_m_employees");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_rooms");

            migrationBuilder.DropTable(
                name: "tb_m_universities");
        }
    }
}
