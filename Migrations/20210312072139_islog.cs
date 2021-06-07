using Microsoft.EntityFrameworkCore.Migrations;

namespace RstateAPI.Migrations
{
    public partial class islog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.AddColumn<string>(
                name: "Islogin",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
