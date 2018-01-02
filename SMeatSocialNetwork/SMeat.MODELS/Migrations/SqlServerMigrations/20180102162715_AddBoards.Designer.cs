﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SMeat.MODELS;
using SMeat.MODELS.Models.Enums;
using System;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180102162715_AddBoards")]
    partial class AddBoards
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SMeat.MODELS.Models.Board", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Chat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Contacts", b =>
                {
                    b.Property<string>("FirstUserId");

                    b.Property<string>("SecondUserId");

                    b.HasKey("FirstUserId", "SecondUserId");

                    b.HasIndex("SecondUserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.GroupChat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GroupChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChatId");

                    b.Property<DateTimeOffset?>("DateTime");

                    b.Property<string>("GroupChatId");

                    b.Property<int>("MessageStatus");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("GroupChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Reply", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoardId");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTimeOffset?>("Birthdate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CustomGenderType");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("GenderType");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("LocationId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PictureUrl");

                    b.Property<int>("RelationshipType");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("WorkplaceId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.UserChat", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("ChatId");

                    b.Property<int>("Permitions");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.UserGroupChat", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("GroupChatId");

                    b.Property<int>("UserRole");

                    b.HasKey("UserId", "GroupChatId");

                    b.HasIndex("GroupChatId");

                    b.ToTable("UserGroupChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Workplace", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("LocationId");

                    b.Property<string>("Position");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("WorkPlaces");
                });

            modelBuilder.Entity("SMeat.MODELS.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("SMeat.MODELS.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("SMeat.MODELS.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("SMeat.MODELS.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("SMeat.MODELS.UserRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("SMeat.MODELS.UserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Chat", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Contacts", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.User", "FirstUser")
                        .WithMany("ContactsTo")
                        .HasForeignKey("FirstUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Models.User", "SecondUser")
                        .WithMany("ContactsFrom")
                        .HasForeignKey("SecondUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Message", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");

                    b.HasOne("SMeat.MODELS.Models.GroupChat", "GroupChat")
                        .WithMany("Messages")
                        .HasForeignKey("GroupChatId");

                    b.HasOne("SMeat.MODELS.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Reply", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.Board", "Board")
                        .WithMany("Replies")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.User", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.Location", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId");

                    b.HasOne("SMeat.MODELS.Models.Workplace", "Workplace")
                        .WithMany("Users")
                        .HasForeignKey("WorkplaceId");
                });

            modelBuilder.Entity("SMeat.MODELS.Models.UserChat", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.Chat", "Chat")
                        .WithMany("UserChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Models.User", "User")
                        .WithMany("UserChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Models.UserGroupChat", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.GroupChat", "GroupChat")
                        .WithMany("UserGroupChats")
                        .HasForeignKey("GroupChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Models.User", "User")
                        .WithMany("UserGroupChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Models.Workplace", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.Location", "Location")
                        .WithMany("Workplaces")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("SMeat.MODELS.UserClaim", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.UserLogin", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.UserRole", b =>
                {
                    b.HasOne("SMeat.MODELS.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.UserRoleClaim", b =>
                {
                    b.HasOne("SMeat.MODELS.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.UserToken", b =>
                {
                    b.HasOne("SMeat.MODELS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
