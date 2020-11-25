using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class AddRecordResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordServerResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WatchedRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    DnsServerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    TimeToLive = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordServerResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordServerResults_DnsServers_DnsServerId",
                        column: x => x.DnsServerId,
                        principalTable: "DnsServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordServerResults_WatchedRecords_WatchedRecordId",
                        column: x => x.WatchedRecordId,
                        principalTable: "WatchedRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordServerResults_DnsServerId",
                table: "RecordServerResults",
                column: "DnsServerId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordServerResults_WatchedRecordId",
                table: "RecordServerResults",
                column: "WatchedRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordServerResults");
        }
    }
}
