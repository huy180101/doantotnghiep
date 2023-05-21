using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hanhkiemUtehy.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "class_id",
                table: "teacher_user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "class_id",
                table: "teacher_user");
        }
    }
}
