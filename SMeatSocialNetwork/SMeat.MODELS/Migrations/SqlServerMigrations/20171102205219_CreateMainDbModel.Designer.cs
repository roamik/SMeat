using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20171102205219_CreateMainDbModel")]
    partial class CreateMainDbModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("SMeat.MODELS.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTimeOffset?>("Birthdate");

                    b.Property<int>("GenderType");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PictureUrl");

                    b.Property<int>("RelationshipType");

                    b.Property<string>("Status");

                    b.Property<string>("WorkplaceId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

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
                        .HasForeignKey("SecondUserId");
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
        }
    }
}
