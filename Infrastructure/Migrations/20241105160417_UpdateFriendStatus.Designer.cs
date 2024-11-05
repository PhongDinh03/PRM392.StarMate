﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ZodiacTinderContext))]
    [Migration("20241105160417_UpdateFriendStatus")]
    partial class UpdateFriendStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.ToTable("Friend", (string)null);
                });

            modelBuilder.Entity("Domain.Models.LikeZodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ZodiacLikeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ZodiacLikeId");

                    b.ToTable("LikeZodiac", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("gender")
                        .IsFixedLength();

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("LikeListId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZodiacId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZodiacId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Zodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DesZodiac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameZodiac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Zodiac", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Friend", b =>
                {
                    b.HasOne("Domain.Models.User", "FriendNavigation")
                        .WithMany("FriendFriendNavigations")
                        .HasForeignKey("FriendId")
                        .IsRequired()
                        .HasConstraintName("FK_Friend_User1");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("FriendUsers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Friend_User");

                    b.Navigation("FriendNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.LikeZodiac", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("LikeZodiacs")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_LikeZodiac_User");

                    b.HasOne("Domain.Models.Zodiac", "ZodiacLike")
                        .WithMany("LikeZodiacs")
                        .HasForeignKey("ZodiacLikeId")
                        .IsRequired()
                        .HasConstraintName("FK_LikeZodiac_Zodiac");

                    b.Navigation("User");

                    b.Navigation("ZodiacLike");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Zodiac", "Zodiac")
                        .WithMany("Users")
                        .HasForeignKey("ZodiacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Zodiac");

                    b.Navigation("Zodiac");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("FriendFriendNavigations");

                    b.Navigation("FriendUsers");

                    b.Navigation("LikeZodiacs");
                });

            modelBuilder.Entity("Domain.Models.Zodiac", b =>
                {
                    b.Navigation("LikeZodiacs");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
