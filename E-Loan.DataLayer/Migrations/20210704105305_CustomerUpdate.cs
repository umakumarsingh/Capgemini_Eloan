using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Loan.DataLayer.Migrations
{
    public partial class CustomerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loanMasters",
                columns: table => new
                {
                    LoanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanName = table.Column<string>(nullable: false),
                    LoanAmount = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BusinessStructure = table.Column<int>(nullable: false),
                    Billing_Indicator = table.Column<int>(nullable: false),
                    Tax_Indicator = table.Column<int>(nullable: false),
                    ContactAddress = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AppliedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanMasters", x => x.LoanId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loanMasters");
        }
    }
}
