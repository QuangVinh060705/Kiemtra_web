using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kiemtra_web.Migrations
{
    /// <inheritdoc />
    public partial class AddGhiChuToGoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ghi_chu",
                table: "Hanghoa",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ghi_chu",
                table: "Hanghoa");
        }
    }

}
