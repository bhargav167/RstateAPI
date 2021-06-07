﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RstateAPI.Data;

namespace RstateAPI.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20210312071009_islogin")]
    partial class islogin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RstateAPI.Entities.BasicDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AllowedTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AnyTimeAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AreaUnit")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AvalableFrom")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Balconey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Bathroom")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("BedInRoom")
                        .HasColumnType("int");

                    b.Property<string>("Bhk")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Brokerage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BrokerageAmt")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BuildUpArea")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CarpetArea")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CommonArea")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConstructionType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DrinkingAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FacilitiesOffered")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorRange")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("GardianAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("LockPeriod")
                        .HasColumnType("int");

                    b.Property<string>("Maintainescharge")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MealAvalable")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NonVegAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("NoticePeriod")
                        .HasColumnType("int");

                    b.Property<string>("OppositeSex")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PGRent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PgFor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PgName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PgSecurityDeposite")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PgSuitedFor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PloatArea")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PossesionType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PropertyManageBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PropertyManageStay")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoomType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecurityAmenities")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecurityDeposite")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecurityDepositeAmt")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SmokingAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ToatalBed")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VisitorAllowed")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("WantTo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Wfr")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Width")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("coverParking")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("furnishedType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("length")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mothlyRent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("openParking")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("plotPrice")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("propertadyAge")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("propertyType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("teneandType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("basicDetails");
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.AddressLocality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LocalityName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("cityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("cityId");

                    b.ToTable("AddressLocality");
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CitynName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("city");
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.Pocket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PocketAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("sectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("sectorId");

                    b.ToTable("Pockets");
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.SearchAddresData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adddresdata")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("addressSearchData");
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("SectorName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("localityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("localityId");

                    b.ToTable("sector");
                });

            modelBuilder.Entity("RstateAPI.Entities.ContactDetails.UserContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OwnerId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UniqueID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("userContact");
                });

            modelBuilder.Entity("RstateAPI.Entities.Images1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("cover")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("tag")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("uniqueID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("RstateAPI.Entities.Locations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Locality")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PlotNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PocketNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Project")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SectorNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("RstateAPI.Entities.PropertyConfiguration.AdminAccessProp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsManageAddress")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsManageCity")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsManageFeild")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsManageLocality")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsManagePocket")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsManageSector")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSetPropImg")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("adminconfigProp");
                });

            modelBuilder.Entity("RstateAPI.Entities.PropertyConfiguration.PropertyConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsBhk")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPropertyType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ageOfProperty")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("anyTimeAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("availableFrom")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("balcony")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("bathRoom")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("bedInRoom")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("brokerage")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("buildArea")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("carpetArea")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("commonAreas")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("constructionStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("coverParking")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("drinkingAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("facilitiesOffered")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("furnishType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("guardianAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("maintainCharge")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("mealAvalable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("monthlyRent")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("nonVegAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("openParking")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("oppositeSexAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("pGName")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("pgFor")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("pgRent")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("propertyManagedBy")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("roomType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("securityDeposite")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("securityType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("smokingAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("suitedFor")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("tenantType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("totalBed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("transactionType")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("visitorsAllowed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("PropertyConfig");
                });

            modelBuilder.Entity("RstateAPI.Entities.SaveModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsLead")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("basicDetailIdId")
                        .HasColumnType("int");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("isDecline")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("locationIdId")
                        .HasColumnType("int");

                    b.Property<string>("price")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("uniqueID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("basicDetailIdId");

                    b.HasIndex("locationIdId");

                    b.ToTable("PostPropertyModel");
                });

            modelBuilder.Entity("RstateAPI.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("BrokerFirm")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirmAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirmAddress1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("GstNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("imagUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("landlineNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("locations")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mobNo1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mobNo2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mobNo3")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mobNo4")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("reraNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tokenid")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("userId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.AddressLocality", b =>
                {
                    b.HasOne("RstateAPI.Entities.CityData.City", "city")
                        .WithMany()
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.Pocket", b =>
                {
                    b.HasOne("RstateAPI.Entities.CityData.Sector", "sector")
                        .WithMany()
                        .HasForeignKey("sectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RstateAPI.Entities.CityData.Sector", b =>
                {
                    b.HasOne("RstateAPI.Entities.CityData.AddressLocality", "locality")
                        .WithMany()
                        .HasForeignKey("localityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RstateAPI.Entities.SaveModel", b =>
                {
                    b.HasOne("RstateAPI.Entities.BasicDetails", "basicDetailId")
                        .WithMany()
                        .HasForeignKey("basicDetailIdId");

                    b.HasOne("RstateAPI.Entities.Locations", "locationId")
                        .WithMany()
                        .HasForeignKey("locationIdId");
                });
#pragma warning restore 612, 618
        }
    }
}
