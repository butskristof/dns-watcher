using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class AddRecordPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "WatchedRecords",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "WatchedRecords");
        }
    }
}
