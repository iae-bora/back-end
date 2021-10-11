﻿// <auto-generated />
using System;
using IaeBoraLibrary.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IaeBoraLibrary.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20211008192612_MigrationDB_V1")]
    partial class MigrationDB_V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IaeBoraLibrary.Model.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Food")
                        .HasColumnType("int");

                    b.Property<int>("HaveChildren")
                        .HasColumnType("int");

                    b.Property<int>("Movies")
                        .HasColumnType("int");

                    b.Property<int>("Musics")
                        .HasColumnType("int");

                    b.Property<int>("PlacesCount")
                        .HasColumnType("int");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<DateTime>("RouteDateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sports")
                        .HasColumnType("int");

                    b.Property<int>("Teams")
                        .HasColumnType("int");

                    b.Property<int>("UserAge")
                        .HasColumnType("int");

                    b.Property<string>("UserGoogleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserGoogleId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.OpeningHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndHour")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartHour")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<int?>("RestaurantCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RouteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserGoogleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserGoogleId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.TouristPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("DistanceFromOrigin")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndHour")
                        .HasColumnType("datetime2");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int?>("OpeningHoursId")
                        .HasColumnType("int");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartHour")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OpeningHoursId");

                    b.HasIndex("RouteId");

                    b.ToTable("TouristPoints");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.User", b =>
                {
                    b.Property<string>("GoogleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GoogleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.Answer", b =>
                {
                    b.HasOne("IaeBoraLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserGoogleId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.FeedBack", b =>
                {
                    b.HasOne("IaeBoraLibrary.Model.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.OpeningHours", b =>
                {
                    b.HasOne("IaeBoraLibrary.Model.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.Route", b =>
                {
                    b.HasOne("IaeBoraLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserGoogleId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IaeBoraLibrary.Model.TouristPoint", b =>
                {
                    b.HasOne("IaeBoraLibrary.Model.OpeningHours", "OpeningHours")
                        .WithMany()
                        .HasForeignKey("OpeningHoursId");

                    b.HasOne("IaeBoraLibrary.Model.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.Navigation("OpeningHours");

                    b.Navigation("Route");
                });
#pragma warning restore 612, 618
        }
    }
}