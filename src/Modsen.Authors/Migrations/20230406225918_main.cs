using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsen.Authors.Migrations
{
    /// <inheritdoc />
    public partial class main : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Die",
                table: "Authors",
                newName: "DieDate");

            migrationBuilder.RenameColumn(
                name: "Born",
                table: "Authors",
                newName: "BornDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DieDate",
                table: "Authors",
                newName: "Die");

            migrationBuilder.RenameColumn(
                name: "BornDate",
                table: "Authors",
                newName: "Born");
        }
    }
}
