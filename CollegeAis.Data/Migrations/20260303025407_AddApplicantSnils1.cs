using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicantSnils1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SNILS",
                table: "Applicants",
                newName: "Snils");

            migrationBuilder.AlterColumn<string>(
                name: "Snils",
                table: "Applicants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

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

            migrationBuilder.RenameColumn(
                name: "Snils",
                table: "Applicants",
                newName: "SNILS");

            migrationBuilder.AlterColumn<string>(
                name: "SNILS",
                table: "Applicants",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
