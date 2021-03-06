﻿// <auto-generated />
using System;
using DnsWatcher.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DnsWatcher.Persistence.Migrations
{
    [DbContext(typeof(DnsWatcherDbContext))]
    [Migration("20201125222118_AddRecords")]
    partial class AddRecords
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Domains.WatchedDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("WatchedDomains");
                });

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Domains.WatchedRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExpectedIpAddress")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("ExpectedPort")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RecordType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WatchedDomainId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WatchedDomainId");

                    b.ToTable("WatchedRecords");
                });

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("bytea")
                        .IsFixedLength(true);

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("bytea")
                        .IsFixedLength(true);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Servers.DnsServer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DnsServers");
                });

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Domains.WatchedRecord", b =>
                {
                    b.HasOne("DnsWatcher.Domain.Entities.Domains.WatchedDomain", "WatchedDomain")
                        .WithMany("WatchedRecords")
                        .HasForeignKey("WatchedDomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WatchedDomain");
                });

            modelBuilder.Entity("DnsWatcher.Domain.Entities.Domains.WatchedDomain", b =>
                {
                    b.Navigation("WatchedRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
