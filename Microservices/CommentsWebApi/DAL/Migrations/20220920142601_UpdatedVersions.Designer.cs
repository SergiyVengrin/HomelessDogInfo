﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(CommentContext))]
    [Migration("20220920142601_UpdatedVersions")]
    partial class UpdatedVersions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentID"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("CommentID"), 1L, null, 1L, 2147483647L, null, null);

                    b.Property<DateTime?>("DateTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DogID")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.HasKey("CommentID");

                    b.HasIndex("UserEmail");

                    b.ToTable("Comments", (string)null);

                    b.HasCheckConstraint("dogid_constraint", "\"DogID\" >=0");

                    b.HasCheckConstraint("ratingconstraint", "\"Rating\" >= 1 AND \"Rating\" <= 5");

                    b.HasCheckConstraint("text_constraint", "LENGTH(\"Text\") >=1");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasCheckConstraint("email_constraint", "\"Email\" LIKE '%_@__%.__%'");

                    b.HasCheckConstraint("name_constraint", "LENGTH(\"Username\")>=2");

                    b.HasCheckConstraint("password_constraint", "LENGTH(\"Password\")>=8");
                });

            modelBuilder.Entity("DAL.Entities.Comment", b =>
                {
                    b.HasOne("DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserEmail");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
