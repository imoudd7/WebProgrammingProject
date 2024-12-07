﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241207094417_AddedModels")]
    partial class AddedModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebProject.Models.Appointments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Onay")
                        .HasColumnType("bit");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("ServislerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Ucret")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId");

                    b.HasIndex("ServislerId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("WebProject.Models.Personal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uzmanlık")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Personaller");
                });

            modelBuilder.Entity("WebProject.Models.PersonalAvailabilities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("time");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("time");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId");

                    b.ToTable("Personal_Calisma_Zamanlari");
                });

            modelBuilder.Entity("WebProject.Models.PersonalServices", b =>
                {
                    b.Property<int>("PersonalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonalID"));

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("ServislerId")
                        .HasColumnType("int");

                    b.HasKey("PersonalID");

                    b.HasIndex("PersonalId");

                    b.HasIndex("ServislerId");

                    b.ToTable("Personal_servisleri");
                });

            modelBuilder.Entity("WebProject.Models.Salons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("time");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Salonlar");
                });

            modelBuilder.Entity("WebProject.Models.Services", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Servisler");
                });

            modelBuilder.Entity("WebProject.Models.Appointments", b =>
                {
                    b.HasOne("WebProject.Models.Personal", "Employee")
                        .WithMany()
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProject.Models.Services", "Servisler")
                        .WithMany()
                        .HasForeignKey("ServislerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Servisler");
                });

            modelBuilder.Entity("WebProject.Models.Personal", b =>
                {
                    b.HasOne("WebProject.Models.Salons", "Salon")
                        .WithMany("Personals")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("WebProject.Models.PersonalAvailabilities", b =>
                {
                    b.HasOne("WebProject.Models.Personal", "Personal")
                        .WithMany("Personal_Zamanlari")
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("WebProject.Models.PersonalServices", b =>
                {
                    b.HasOne("WebProject.Models.Personal", "Personal")
                        .WithMany("Personal_Servisler")
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProject.Models.Services", "Servisler")
                        .WithMany()
                        .HasForeignKey("ServislerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");

                    b.Navigation("Servisler");
                });

            modelBuilder.Entity("WebProject.Models.Services", b =>
                {
                    b.HasOne("WebProject.Models.Salons", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("WebProject.Models.Personal", b =>
                {
                    b.Navigation("Personal_Servisler");

                    b.Navigation("Personal_Zamanlari");
                });

            modelBuilder.Entity("WebProject.Models.Salons", b =>
                {
                    b.Navigation("Personals");
                });
#pragma warning restore 612, 618
        }
    }
}
