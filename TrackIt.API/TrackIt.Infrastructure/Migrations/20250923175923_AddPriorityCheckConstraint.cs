using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_TodoItems_Priority",
                table: "TodoItems",
                sql: "[Priority] BETWEEN 1 AND 3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TodoItems_Priority",
                table: "TodoItems");
        }
    }
}
