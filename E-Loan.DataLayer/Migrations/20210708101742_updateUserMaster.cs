using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Loan.DataLayer.Migrations
{
    public partial class updateUserMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProof",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdProofNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdproofTypes",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProofNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdproofTypes",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdProof",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
