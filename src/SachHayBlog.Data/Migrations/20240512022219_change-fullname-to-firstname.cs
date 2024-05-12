using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SachHayBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class changefullnametofirstname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AppUsers",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AppUsers",
                newName: "FullName");
        }
    }
}
