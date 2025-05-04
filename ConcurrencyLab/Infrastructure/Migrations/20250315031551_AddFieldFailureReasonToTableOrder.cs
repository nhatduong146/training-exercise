using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcurrencyLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldFailureReasonToTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "Orders");
        }
    }
}
