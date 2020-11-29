using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class AddRecordType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordType",
                table: "WatchedRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordType",
                table: "WatchedRecords");
        }
    }
}
