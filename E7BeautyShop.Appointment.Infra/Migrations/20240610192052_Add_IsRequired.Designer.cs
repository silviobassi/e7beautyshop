﻿// <auto-generated />
using System;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace E7BeautyShop.Appointment.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240610192052_Add_IsRequired")]
    partial class Add_IsRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.Catalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.DayRest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DayOnWeek")
                        .HasColumnType("integer");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("DayRest");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.OfficeHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CatalogId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAndHour")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ScheduleId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("OfficeHours");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.Catalog", b =>
                {
                    b.OwnsOne("E7BeautyShop.Appointment.Core.ServiceDescription", "ServiceDescription", b1 =>
                        {
                            b1.Property<Guid>("CatalogId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_Name");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric")
                                .HasColumnName("Description_Price");

                            b1.HasKey("CatalogId");

                            b1.ToTable("Catalogs");

                            b1.WithOwner()
                                .HasForeignKey("CatalogId");
                        });

                    b.Navigation("ServiceDescription");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.DayRest", b =>
                {
                    b.HasOne("E7BeautyShop.Appointment.Core.Schedule", null)
                        .WithMany("DaysRest")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.OfficeHour", b =>
                {
                    b.HasOne("E7BeautyShop.Appointment.Core.Catalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogId");

                    b.HasOne("E7BeautyShop.Appointment.Core.Schedule", null)
                        .WithMany("OfficeHours")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("E7BeautyShop.Appointment.Core.CustomerId", "CustomerId", b1 =>
                        {
                            b1.Property<Guid>("OfficeHourId")
                                .HasColumnType("uuid");

                            b1.Property<Guid?>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("Customer_Value");

                            b1.HasKey("OfficeHourId");

                            b1.ToTable("OfficeHours");

                            b1.WithOwner()
                                .HasForeignKey("OfficeHourId");
                        });

                    b.Navigation("Catalog");

                    b.Navigation("CustomerId");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.Schedule", b =>
                {
                    b.OwnsOne("E7BeautyShop.Appointment.Core.ProfessionalId", "ProfessionalId", b1 =>
                        {
                            b1.Property<Guid>("ScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("Professional_Value");

                            b1.HasKey("ScheduleId");

                            b1.ToTable("Schedules");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");
                        });

                    b.OwnsOne("E7BeautyShop.Appointment.Core.Weekday", "Weekday", b1 =>
                        {
                            b1.Property<Guid>("ScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<TimeSpan>("EndAt")
                                .HasColumnType("interval")
                                .HasColumnName("Weekday_EndAt");

                            b1.Property<TimeSpan>("StartAt")
                                .HasColumnType("interval")
                                .HasColumnName("Weekday_StartAt");

                            b1.HasKey("ScheduleId");

                            b1.ToTable("Schedules");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");
                        });

                    b.OwnsOne("E7BeautyShop.Appointment.Core.Weekend", "Weekend", b1 =>
                        {
                            b1.Property<Guid>("ScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<TimeSpan>("EndAt")
                                .HasColumnType("interval")
                                .HasColumnName("Weekend_EndAt");

                            b1.Property<TimeSpan>("StartAt")
                                .HasColumnType("interval")
                                .HasColumnName("Weekend_StartAt");

                            b1.HasKey("ScheduleId");

                            b1.ToTable("Schedules");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");
                        });

                    b.Navigation("ProfessionalId");

                    b.Navigation("Weekday");

                    b.Navigation("Weekend");
                });

            modelBuilder.Entity("E7BeautyShop.Appointment.Core.Schedule", b =>
                {
                    b.Navigation("DaysRest");

                    b.Navigation("OfficeHours");
                });
#pragma warning restore 612, 618
        }
    }
}
