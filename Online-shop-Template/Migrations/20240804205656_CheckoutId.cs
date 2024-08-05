using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_shop_Template.Migrations
{
    public partial class CheckoutId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckoutId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "Orders");
        }
    }
}
