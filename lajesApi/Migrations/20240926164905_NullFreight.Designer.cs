﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace lajesApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240926164905_NullFreight")]
    partial class NullFreight
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CostumerId")
                        .HasColumnType("int");

                    b.Property<string>("CostumerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Footage")
                        .HasColumnType("double");

                    b.Property<int?>("FreightId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("FreightId");

                    b.ToTable("budgets");
                });

            modelBuilder.Entity("BudgetSlab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<double>("Length")
                        .HasColumnType("double");

                    b.Property<int>("SlabId")
                        .HasColumnType("int");

                    b.Property<int>("SlabsNumber")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.ToTable("budgetSlabs");
                });

            modelBuilder.Entity("BudgetSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Administration")
                        .HasColumnType("double");

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<double>("Contribution")
                        .HasColumnType("double");

                    b.Property<double>("Extra")
                        .HasColumnType("double");

                    b.Property<double>("FreightWeight")
                        .HasColumnType("double");

                    b.Property<double>("Taxes")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("budgetSummaries");
                });

            modelBuilder.Entity("Costumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("AddressNumber")
                        .HasColumnType("int");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CnpjCpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("PJ")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("costumers");
                });

            modelBuilder.Entity("Freight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("freights");
                });

            modelBuilder.Entity("Slab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("slabs");
                });

            modelBuilder.Entity("Budget", b =>
                {
                    b.HasOne("Freight", "Freight")
                        .WithMany()
                        .HasForeignKey("FreightId");

                    b.Navigation("Freight");
                });

            modelBuilder.Entity("BudgetSlab", b =>
                {
                    b.HasOne("Budget", null)
                        .WithMany("Slabs")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Budget", b =>
                {
                    b.Navigation("Slabs");
                });
#pragma warning restore 612, 618
        }
    }
}
