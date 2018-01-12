using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20171031235911_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SMeat.MODELS.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTimeOffset?>("Birthdate");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PictureUrl");

                    b.Property<string>("Status");

                    b.Property<int>("WorkplaceId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
