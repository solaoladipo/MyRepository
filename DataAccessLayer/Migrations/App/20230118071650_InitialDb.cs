using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations.App
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiary",
                columns: table => new
                {
                    BeneficiaryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contactperson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RCno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vatno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bankname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bankacctno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sortcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetupOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Createdby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datecreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.BeneficiaryID);
                });

            migrationBuilder.CreateTable(
                name: "CodingHead",
                columns: table => new
                {
                    ColdingHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Refno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Documentno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WHT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sendforapproval = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    approved = table.Column<bool>(type: "bit", nullable: false),
                    createdby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datecreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EarlierAdditionorDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AnotherCharges = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingHead", x => x.ColdingHeadId);
                });

            migrationBuilder.CreateTable(
                name: "RefTag",
                columns: table => new
                {
                    RefnoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serialNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTag", x => x.RefnoID);
                });

            migrationBuilder.CreateTable(
                name: "CodingDetail",
                columns: table => new
                {
                    ColdingDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColdingHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Particulars = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WHT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Allocate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingHeadColdingHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingDetail", x => x.ColdingDetailsId);
                    table.ForeignKey(
                        name: "FK_CodingDetail_CodingHead_CodingHeadColdingHeadId",
                        column: x => x.CodingHeadColdingHeadId,
                        principalTable: "CodingHead",
                        principalColumn: "ColdingHeadId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingDetail_CodingHeadColdingHeadId",
                table: "CodingDetail",
                column: "CodingHeadColdingHeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiary");

            migrationBuilder.DropTable(
                name: "CodingDetail");

            migrationBuilder.DropTable(
                name: "RefTag");

            migrationBuilder.DropTable(
                name: "CodingHead");
        }
    }
}
