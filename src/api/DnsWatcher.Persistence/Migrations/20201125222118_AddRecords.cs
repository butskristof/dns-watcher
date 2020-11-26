using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
    public partial class AddRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchedRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordType = table.Column<string>(type: "text", nullable: false),
                    ExpectedIpAddress = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ExpectedPort = table.Column<int>(type: "integer", nullable: false),
                    WatchedDomainId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchedRecords_WatchedDomains_WatchedDomainId",
                        column: x => x.WatchedDomainId,
                        principalTable: "WatchedDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedRecords_WatchedDomainId",
                table: "WatchedRecords",
                column: "WatchedDomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedRecords");
        }
    }
}
