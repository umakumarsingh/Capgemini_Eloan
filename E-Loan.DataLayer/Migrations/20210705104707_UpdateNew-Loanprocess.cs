using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Loan.DataLayer.Migrations
{
    public partial class UpdateNewLoanprocess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loanProcesstrans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcresofLand = table.Column<long>(nullable: false),
                    LandValueinRs = table.Column<long>(nullable: false),
                    AppraisedBy = table.Column<string>(nullable: true),
                    ValuationDate = table.Column<DateTime>(nullable: false),
                    AddressofProperty = table.Column<string>(nullable: true),
                    SuggestedAmount = table.Column<long>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false),
                    LoanId = table.Column<int>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanProcesstrans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loanProcesstrans");
        }
    }
}
