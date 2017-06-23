using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Venture.ProjectRead.Data;

namespace Venture.ProjectRead.Data.Migrations
{
    [DbContext(typeof(ProjectReadContext))]
    [Migration("20170621155501_RemoveIdFromTag")]
    partial class RemoveIdFromTag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Venture.ProjectRead.Data.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("PostedOn");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Venture.ProjectRead.Data.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Venture.ProjectRead.Data.Tag", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProjectId");

                    b.HasKey("Name");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Venture.ProjectRead.Data.Comment", b =>
                {
                    b.HasOne("Venture.ProjectRead.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Venture.ProjectRead.Data.Tag", b =>
                {
                    b.HasOne("Venture.ProjectRead.Data.Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
