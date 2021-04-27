﻿// <auto-generated />
using System;
using Funzone.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Funzone.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(FunzoneDbContext))]
    [Migration("20210427070214_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Funzone.Domain.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasMaxLength(2054)
                        .HasColumnType("nvarchar(2054)");

                    b.Property<DateTime?>("EditedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PostedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Funzone.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NickName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Funzone.Domain.ZoneRules.ZoneRule", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ZoneRules");
                });

            modelBuilder.Entity("Funzone.Domain.ZoneUsers.ZoneUser", b =>
                {
                    b.Property<Guid>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsLeave")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ZoneId", "UserId");

                    b.ToTable("ZoneUsers");
                });

            modelBuilder.Entity("Funzone.Domain.Zones.Zone", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Funzone.Domain.Posts.Post", b =>
                {
                    b.OwnsMany("Funzone.Domain.Posts.PostReview", "PostReviews", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Detail")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<DateTime>("ReviewedTime")
                                .HasColumnType("datetime2");

                            b1.Property<Guid?>("ReviewerId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("PostReviews");

                            b1.WithOwner()
                                .HasForeignKey("PostId");

                            b1.OwnsOne("Funzone.Domain.Posts.PostStatus", "PostStatus", b2 =>
                                {
                                    b2.Property<Guid>("PostReviewPostId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("PostReviewId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Value")
                                        .HasMaxLength(20)
                                        .HasColumnType("nvarchar(20)")
                                        .HasColumnName("PostStatus");

                                    b2.HasKey("PostReviewPostId", "PostReviewId");

                                    b2.ToTable("PostReviews");

                                    b2.WithOwner()
                                        .HasForeignKey("PostReviewPostId", "PostReviewId");
                                });

                            b1.Navigation("PostStatus");
                        });

                    b.OwnsOne("Funzone.Domain.Posts.PostStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Status");

                            b1.HasKey("PostId");

                            b1.ToTable("Posts");

                            b1.WithOwner()
                                .HasForeignKey("PostId");
                        });

                    b.OwnsOne("Funzone.Domain.Posts.PostType", "Type", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Type");

                            b1.HasKey("PostId");

                            b1.ToTable("Posts");

                            b1.WithOwner()
                                .HasForeignKey("PostId");
                        });

                    b.Navigation("PostReviews");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Funzone.Domain.Users.User", b =>
                {
                    b.OwnsOne("Funzone.Domain.Users.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("EmailAddress");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("EmailAddress");
                });

            modelBuilder.Entity("Funzone.Domain.ZoneUsers.ZoneUser", b =>
                {
                    b.OwnsOne("Funzone.Domain.ZoneUsers.ZoneUserRole", "Role", b1 =>
                        {
                            b1.Property<Guid>("ZoneUserZoneId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ZoneUserUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Role");

                            b1.HasKey("ZoneUserZoneId", "ZoneUserUserId");

                            b1.ToTable("ZoneUsers");

                            b1.WithOwner()
                                .HasForeignKey("ZoneUserZoneId", "ZoneUserUserId");
                        });

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Funzone.Domain.Zones.Zone", b =>
                {
                    b.OwnsOne("Funzone.Domain.Zones.ZoneStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("ZoneId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Status");

                            b1.HasKey("ZoneId");

                            b1.ToTable("Zones");

                            b1.WithOwner()
                                .HasForeignKey("ZoneId");
                        });

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
