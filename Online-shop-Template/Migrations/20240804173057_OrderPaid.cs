using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_shop_Template.Migrations
{
    public partial class OrderPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderPaid",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPaid",
                table: "Orders");
        }
    }
}
