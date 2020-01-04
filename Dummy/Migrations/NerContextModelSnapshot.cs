﻿// <auto-generated />
using System;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dummy.Migrations
{
    [DbContext(typeof(NerContext))]
    partial class NerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Demo4NER.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("BusinessImage");

                    b.Property<string>("Contact");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<DateTime>("FeaturedEndDate");

                    b.Property<bool>("HasDiscounts");

                    b.Property<bool>("IsFeatured");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<float>("Rating");

                    b.Property<string>("Schedule");

                    b.Property<int?>("UserId");

                    b.Property<string>("WrittenAddress");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Businesses");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Business");
                });

            modelBuilder.Entity("Demo4NER.Models.Click", b =>
                {
                    b.Property<DateTime>("Date");

                    b.Property<int?>("BusinessId");

                    b.Property<int?>("UserId");

                    b.HasKey("Date");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("Cliks");
                });

            modelBuilder.Entity("Demo4NER.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BusinessId");

                    b.Property<string>("Description");

                    b.Property<float>("Percentage");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Demo4NER.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BusinessId");

                    b.Property<DateTime>("End");

                    b.Property<int>("Price");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Demo4NER.Models.Link", b =>
                {
                    b.Property<string>("URL")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BusinessId");

                    b.Property<string>("Name");

                    b.HasKey("URL");

                    b.HasIndex("BusinessId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Demo4NER.Models.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BusinessId");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("Demo4NER.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BusinessId");

                    b.Property<string>("BusinessName");

                    b.Property<string>("Comment");

                    b.Property<double>("Rating");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Demo4NER.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Demo4NER.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contact");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Nationality");

                    b.Property<string>("Password");

                    b.Property<byte[]>("UserImage");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Demo4NER.Models.Establishment", b =>
                {
                    b.HasBaseType("Demo4NER.Models.Business");

                    b.Property<int>("Category");

                    b.HasDiscriminator().HasValue("Establishment");
                });

            modelBuilder.Entity("Demo4NER.Models.Service", b =>
                {
                    b.HasBaseType("Demo4NER.Models.Business");

                    b.Property<int?>("UserId1");

                    b.HasIndex("UserId1");

                    b.HasDiscriminator().HasValue("Service");
                });

            modelBuilder.Entity("Demo4NER.Models.Business", b =>
                {
                    b.HasOne("Demo4NER.Models.User")
                        .WithMany("Businesses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Demo4NER.Models.Click", b =>
                {
                    b.HasOne("Demo4NER.Models.Business")
                        .WithMany("Clicks")
                        .HasForeignKey("BusinessId");

                    b.HasOne("Demo4NER.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Demo4NER.Models.Discount", b =>
                {
                    b.HasOne("Demo4NER.Models.Business")
                        .WithMany("Discounts")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("Demo4NER.Models.Feature", b =>
                {
                    b.HasOne("Demo4NER.Models.Business")
                        .WithMany("Features")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("Demo4NER.Models.Link", b =>
                {
                    b.HasOne("Demo4NER.Models.Business")
                        .WithMany("Links")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("Demo4NER.Models.Promotion", b =>
                {
                    b.HasOne("Demo4NER.Models.Business")
                        .WithMany("Promotions")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("Demo4NER.Models.Review", b =>
                {
                    b.HasOne("Demo4NER.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId");

                    b.HasOne("Demo4NER.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Demo4NER.Models.Tag", b =>
                {
                    b.HasOne("Demo4NER.Models.Service")
                        .WithMany("Tags")
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("Demo4NER.Models.Service", b =>
                {
                    b.HasOne("Demo4NER.Models.User")
                        .WithMany("Services")
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
