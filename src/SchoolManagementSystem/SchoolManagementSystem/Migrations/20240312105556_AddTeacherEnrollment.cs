using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEnrollment_Classes_ClassId",
                table: "TeacherEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEnrollment_Teachers_TeacherId",
                table: "TeacherEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherEnrollment",
                table: "TeacherEnrollment");

            migrationBuilder.RenameTable(
                name: "TeacherEnrollment",
                newName: "TeacherEnrollments");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherEnrollment_ClassId",
                table: "TeacherEnrollments",
                newName: "IX_TeacherEnrollments_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherEnrollments",
                table: "TeacherEnrollments",
                columns: new[] { "TeacherId", "ClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEnrollments_Classes_ClassId",
                table: "TeacherEnrollments",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEnrollments_Teachers_TeacherId",
                table: "TeacherEnrollments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEnrollments_Classes_ClassId",
                table: "TeacherEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEnrollments_Teachers_TeacherId",
                table: "TeacherEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherEnrollments",
                table: "TeacherEnrollments");

            migrationBuilder.RenameTable(
                name: "TeacherEnrollments",
                newName: "TeacherEnrollment");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherEnrollments_ClassId",
                table: "TeacherEnrollment",
                newName: "IX_TeacherEnrollment_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherEnrollment",
                table: "TeacherEnrollment",
                columns: new[] { "TeacherId", "ClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEnrollment_Classes_ClassId",
                table: "TeacherEnrollment",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEnrollment_Teachers_TeacherId",
                table: "TeacherEnrollment",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
