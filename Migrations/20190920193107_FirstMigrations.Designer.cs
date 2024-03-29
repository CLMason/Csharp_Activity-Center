﻿// <auto-generated />
using System;
using ActivityCenterI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ActivityCenterI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20190920193107_FirstMigrations")]
    partial class FirstMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ActivityCenterI.Models.Join", b =>
                {
                    b.Property<int>("JoinId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PartyId");

                    b.Property<int>("UserId");

                    b.HasKey("JoinId");

                    b.HasIndex("PartyId");

                    b.HasIndex("UserId");

                    b.ToTable("Joins");
                });

            modelBuilder.Entity("ActivityCenterI.Models.Party", b =>
                {
                    b.Property<int>("PartyId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Duration");

                    b.Property<DateTime>("PartyDate");

                    b.Property<string>("PartyName")
                        .IsRequired();

                    b.Property<string>("PartyTime")
                        .IsRequired();

                    b.Property<int>("PlannerId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("PartyId");

                    b.HasIndex("PlannerId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("ActivityCenterI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ActivityCenterI.Models.Join", b =>
                {
                    b.HasOne("ActivityCenterI.Models.Party", "JoinedParty")
                        .WithMany("AttendingUsers")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ActivityCenterI.Models.User", "Joiner")
                        .WithMany("AttendingParties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ActivityCenterI.Models.Party", b =>
                {
                    b.HasOne("ActivityCenterI.Models.User", "Planner")
                        .WithMany("PlannedParties")
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
