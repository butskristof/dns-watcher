using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class FixTtlPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedPort",
                table: "WatchedRecords",
                newName: "ExpectedTimeToLive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedTimeToLive",
                table: "WatchedRecords",
                newName: "ExpectedPort");
        }
    }
}
