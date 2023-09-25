using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementApp.Migrations
{
    public partial class AlterAccountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "tb_m_accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "tb_m_accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
