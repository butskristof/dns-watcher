using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DnsWatcher.Persistence.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"DnsServers",
				table => new
				{
					Id = table.Column<Guid>("uuid", nullable: false),
					Name = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
					IpAddress = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
					Port = table.Column<int>("integer", nullable: false),
					CreatedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					CreatedOn = table.Column<DateTime>("timestamp without time zone", nullable: false),
					ModifiedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					ModifiedOn = table.Column<DateTime>("timestamp without time zone", nullable: true),
					DeletedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					DeletedOn = table.Column<DateTime>("timestamp without time zone", nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_DnsServers", x => x.Id); });

			migrationBuilder.CreateTable(
				"Users",
				table => new
				{
					Id = table.Column<Guid>("uuid", nullable: false),
					Username = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
					PasswordHash = table.Column<byte[]>("bytea", fixedLength: true, maxLength: 64, nullable: false),
					PasswordSalt = table.Column<byte[]>("bytea", fixedLength: true, maxLength: 128, nullable: false),
					Active = table.Column<bool>("boolean", nullable: false, defaultValue: false),
					CreatedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					CreatedOn = table.Column<DateTime>("timestamp without time zone", nullable: false),
					ModifiedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					ModifiedOn = table.Column<DateTime>("timestamp without time zone", nullable: true),
					DeletedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					DeletedOn = table.Column<DateTime>("timestamp without time zone", nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

			migrationBuilder.CreateTable(
				"WatchedDomains",
				table => new
				{
					Id = table.Column<Guid>("uuid", nullable: false),
					DomainName = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
					CreatedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					CreatedOn = table.Column<DateTime>("timestamp without time zone", nullable: false),
					ModifiedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					ModifiedOn = table.Column<DateTime>("timestamp without time zone", nullable: true),
					DeletedBy = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
					DeletedOn = table.Column<DateTime>("timestamp without time zone", nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_WatchedDomains", x => x.Id); });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"DnsServers");

			migrationBuilder.DropTable(
				"Users");

			migrationBuilder.DropTable(
				"WatchedDomains");
		}
	}
}