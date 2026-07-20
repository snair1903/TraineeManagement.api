using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagement.api.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "ProcessingJobs");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "ProcessingJobs");

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "ProcessingJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ErrorSummary",
                table: "ProcessingJobs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LearningTasks",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedTechStack",
                table: "LearningTasks",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningTasks",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "ProcessingJobs");

            migrationBuilder.DropColumn(
                name: "ErrorSummary",
                table: "ProcessingJobs");

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "ProcessingJobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "ProcessingJobs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LearningTasks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedTechStack",
                table: "LearningTasks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningTasks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);
        }
    }
}
