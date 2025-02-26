using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.BackendServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixmodel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Functions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Functions");
        }
    }
}
