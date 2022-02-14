using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_test.Migrations
{
    public partial class updatenoti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "NotiUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "NotiUser");
        }
    }
}
