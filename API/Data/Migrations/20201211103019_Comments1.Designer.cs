﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201211103019_Comments1")]
    partial class Comments1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Comment", b =>
                {
                    b.Property<int>("CommenterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommentedPhotoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("CommenterId", "CommentedPhotoId");

                    b.HasIndex("CommentedPhotoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("API.Entities.DisLike", b =>
                {
                    b.Property<int>("DisLikerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DisLikedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DisLikerId", "DisLikedId");

                    b.HasIndex("DisLikedId");

                    b.ToTable("DisLikes");
                });

            modelBuilder.Entity("API.Entities.Like", b =>
                {
                    b.Property<int>("LikerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LikedId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LikerId", "LikedId");

                    b.HasIndex("LikedId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.Comment", b =>
                {
                    b.HasOne("API.Entities.Photo", "CommentedPhoto")
                        .WithMany("Commenters")
                        .HasForeignKey("CommentedPhotoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Commenter")
                        .WithMany("Commentees")
                        .HasForeignKey("CommenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CommentedPhoto");

                    b.Navigation("Commenter");
                });

            modelBuilder.Entity("API.Entities.DisLike", b =>
                {
                    b.HasOne("API.Entities.Photo", "DisLiked")
                        .WithMany("DisLikers")
                        .HasForeignKey("DisLikedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "DisLiker")
                        .WithMany("DisLikees")
                        .HasForeignKey("DisLikerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DisLiked");

                    b.Navigation("DisLiker");
                });

            modelBuilder.Entity("API.Entities.Like", b =>
                {
                    b.HasOne("API.Entities.Photo", "Liked")
                        .WithMany("Likers")
                        .HasForeignKey("LikedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Liker")
                        .WithMany("Likees")
                        .HasForeignKey("LikerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Liked");

                    b.Navigation("Liker");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Photos")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Commentees");

                    b.Navigation("DisLikees");

                    b.Navigation("Likees");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Navigation("Commenters");

                    b.Navigation("DisLikers");

                    b.Navigation("Likers");
                });
#pragma warning restore 612, 618
        }
    }
}
