using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class RenameValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedIpAddress",
                table: "WatchedRecords",
                newName: "ExpectedValue");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "RecordServerResults",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedValue",
                table: "WatchedRecords",
                newName: "ExpectedIpAddress");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "RecordServerResults",
                newName: "IpAddress");
        }
    }
}
