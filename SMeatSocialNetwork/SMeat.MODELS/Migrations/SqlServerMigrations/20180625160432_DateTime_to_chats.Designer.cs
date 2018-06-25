﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SMeat.MODELS;
using SMeat.MODELS.Enums;
using System;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180625160432_DateTime_to_chats")]
    partial class DateTime_to_chats
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SMeat.MODELS.Entities.Board", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MadeById");

                    b.Property<DateTimeOffset>("MadeTime");

                    b.Property<string>("Name");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("MadeById");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.BoardDislike", b =>
                {
                    b.Property<string>("BoardId");

                    b.Property<string>("DislikeFromId");

                    b.HasKey("BoardId", "DislikeFromId");

                    b.HasIndex("DislikeFromId");

                    b.ToTable("BoardDislike");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.BoardLike", b =>
                {
                    b.Property<string>("BoardId");

                    b.Property<string>("LikeFromId");

                    b.HasKey("BoardId", "LikeFromId");

                    b.HasIndex("LikeFromId");

                    b.ToTable("BoardLike");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Chat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("MadeTime");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Friends", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("FriendId");

                    b.Property<int>("Status");

                    b.HasKey("UserId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.GroupChat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GroupChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Message", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.Reply", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoardId");

                    b.Property<DateTimeOffset>("DateTime");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.ReplyReply", b =>
                {
                    b.Property<string>("ReplyId");

                    b.Property<string>("ReplyToId");

                    b.HasKey("ReplyId", "ReplyToId");

                    b.HasIndex("ReplyToId");

                    b.ToTable("ReplyReply");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Role", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.User", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.UserChat", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("ChatId");

                    b.Property<int>("Permitions");

                    b.Property<int>("Status");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserClaim", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.UserGroupChat", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("GroupChatId");

                    b.Property<int>("UserRole");

                    b.HasKey("UserId", "GroupChatId");

                    b.HasIndex("GroupChatId");

                    b.ToTable("UserGroupChats");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserLogin", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserRoleClaim", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.UserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Workplace", b =>
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

            modelBuilder.Entity("SMeat.MODELS.Entities.Board", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User", "MadeBy")
                        .WithMany()
                        .HasForeignKey("MadeById");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.BoardDislike", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Board", "Board")
                        .WithMany("Dislikes")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SMeat.MODELS.Entities.User", "DislikeFrom")
                        .WithMany()
                        .HasForeignKey("DislikeFromId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.BoardLike", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Board", "Board")
                        .WithMany("Likes")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SMeat.MODELS.Entities.User", "LikeFrom")
                        .WithMany()
                        .HasForeignKey("LikeFromId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Chat", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Friends", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User", "Friend")
                        .WithMany("ContactsIAddedTo")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SMeat.MODELS.Entities.User", "User")
                        .WithMany("ContactsAddedByMe")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Message", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");

                    b.HasOne("SMeat.MODELS.Entities.GroupChat", "GroupChat")
                        .WithMany("Messages")
                        .HasForeignKey("GroupChatId");

                    b.HasOne("SMeat.MODELS.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Reply", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Board", "Board")
                        .WithMany("Replies")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.ReplyReply", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Reply", "Reply")
                        .WithMany("ReplyTo")
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SMeat.MODELS.Entities.Reply", "ReplyTo")
                        .WithMany()
                        .HasForeignKey("ReplyToId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.User", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Location", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId");

                    b.HasOne("SMeat.MODELS.Entities.Workplace", "Workplace")
                        .WithMany("Users")
                        .HasForeignKey("WorkplaceId");
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserChat", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Chat", "Chat")
                        .WithMany("UserChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Entities.User", "User")
                        .WithMany("UserChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserClaim", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserGroupChat", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.GroupChat", "GroupChat")
                        .WithMany("UserGroupChats")
                        .HasForeignKey("GroupChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Entities.User", "User")
                        .WithMany("UserGroupChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserLogin", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserRole", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SMeat.MODELS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserRoleClaim", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.UserToken", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SMeat.MODELS.Entities.Workplace", b =>
                {
                    b.HasOne("SMeat.MODELS.Entities.Location", "Location")
                        .WithMany("Workplaces")
                        .HasForeignKey("LocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
