using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicantDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SNILS",
                table: "Applicants");

            migrationBuilder.CreateTable(
                name: "ApplicantAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationAddress = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    ActualAddress = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    NeedsDormitory = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantAddresses_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    StudyForm = table.Column<int>(type: "integer", nullable: false),
                    FundingBasis = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantApplications_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantApplications_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantEducationDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocType = table.Column<int>(type: "integer", nullable: false),
                    FinishedClasses = table.Column<int>(type: "integer", nullable: true),
                    Series = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IssuedBy = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    AverageScore = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantEducationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantEducationDocuments_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantParentContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantParentContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantParentContacts_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantPassports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Series = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    IssuedBy = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DivisionCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Citizenship = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Inn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantPassports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantPassports_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantAddresses_ApplicantId",
                table: "ApplicantAddresses",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantApplications_ApplicantId_Priority",
                table: "ApplicantApplications",
                columns: new[] { "ApplicantId", "Priority" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantApplications_ProgramId",
                table: "ApplicantApplications",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantEducationDocuments_ApplicantId",
                table: "ApplicantEducationDocuments",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantParentContacts_ApplicantId",
                table: "ApplicantParentContacts",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantPassports_ApplicantId",
                table: "ApplicantPassports",
                column: "ApplicantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantAddresses");

            migrationBuilder.DropTable(
                name: "ApplicantApplications");

            migrationBuilder.DropTable(
                name: "ApplicantEducationDocuments");

            migrationBuilder.DropTable(
                name: "ApplicantParentContacts");

            migrationBuilder.DropTable(
                name: "ApplicantPassports");

            migrationBuilder.AddColumn<string>(
                name: "SNILS",
                table: "Applicants",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }
    }
}
