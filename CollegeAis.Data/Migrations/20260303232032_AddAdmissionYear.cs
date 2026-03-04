using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionYear",
                table: "Applicants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionYear",
                table: "Applicants");
        }
    }
}
