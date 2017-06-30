using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Venture.TeamRead.Data;

namespace Venture.TeamRead.Data.Migrations
{
    [DbContext(typeof(TeamReadContext))]
    [Migration("20170630220515_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Venture.TeamRead.Data.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PostedOn");

                    b.Property<Guid>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Venture.TeamRead.Data.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ProjectOwnerId");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Venture.TeamRead.Data.Entities.TeamMembership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<Guid>("TeamId");

                    b.Property<Guid>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMemberships");
                });

            modelBuilder.Entity("Venture.TeamRead.Data.Entities.Comment", b =>
                {
                    b.HasOne("Venture.TeamRead.Data.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Venture.TeamRead.Data.Entities.TeamMembership", b =>
                {
                    b.HasOne("Venture.TeamRead.Data.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
