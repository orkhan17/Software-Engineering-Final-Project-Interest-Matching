﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.API.Data;

namespace Project.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Project.API.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birth_date");

                    b.Property<DateTime>("Created_date");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Last_active");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Project.API.Models.Music_type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Music_types");
                });

            modelBuilder.Entity("Project.API.Models.Music_type_account", b =>
                {
                    b.Property<int>("Account_Id");

                    b.Property<int>("Music_type_id");

                    b.Property<int>("Id");

                    b.HasKey("Account_Id", "Music_type_id");

                    b.HasIndex("Music_type_id");

                    b.ToTable("Music_type_accounts");
                });

            modelBuilder.Entity("Project.API.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("Created_date");

                    b.Property<string>("Text");

                    b.Property<string>("Video_link");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Project.API.Models.Post_Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("Post_id");

                    b.HasKey("Id");

                    b.ToTable("Post_Likes");
                });

            modelBuilder.Entity("Project.API.Models.Visited_profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("Following_AccountId");

                    b.HasKey("Id");

                    b.ToTable("Visited_profiles");
                });

            modelBuilder.Entity("Project.API.Models.Music_type_account", b =>
                {
                    b.HasOne("Project.API.Models.Account", "Account")
                        .WithMany("Accounts")
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Project.API.Models.Music_type", "Music_type")
                        .WithMany("Music_types")
                        .HasForeignKey("Music_type_id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Project.API.Models.Post", b =>
                {
                    b.HasOne("Project.API.Models.Account", "Account")
                        .WithMany("Posts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
