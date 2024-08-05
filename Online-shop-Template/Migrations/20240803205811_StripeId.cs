using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_shop_Template.Migrations
{
    public partial class StripeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripePayId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePayId",
                table: "AspNetUsers");
        }
    }
}
