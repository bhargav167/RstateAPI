using Microsoft.EntityFrameworkCore.Migrations;

namespace RstateAPI.Migrations
{
    public partial class islogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "tokenid",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tokenid",
                table: "AspNetUsers"); 
        }
    }
}
