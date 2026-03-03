using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicantSnils : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SNILS",
                table: "Applicants",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SNILS",
                table: "Applicants");
        }
    }
}
