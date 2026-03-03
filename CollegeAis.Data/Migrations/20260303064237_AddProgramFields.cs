using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationLevel",
                table: "Programs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Programs",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "BaseEducation",
                table: "Programs",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BudgetPlaces",
                table: "Programs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidPlaces",
                table: "Programs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Programs",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudyDuration",
                table: "Programs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseEducation",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "BudgetPlaces",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PaidPlaces",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StudyDuration",
                table: "Programs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Programs",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "EducationLevel",
                table: "Programs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
