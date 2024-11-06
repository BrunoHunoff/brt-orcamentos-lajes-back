﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace lajesApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<double>("Administration")
                        .HasColumnType("double");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Cost")
                        .HasColumnType("double");

                    b.Property<int>("CostumerId")
                        .HasColumnType("int");

                    b.Property<string>("CostumerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Extra")
                        .HasColumnType("double");

                    b.Property<double>("Footage")
                        .HasColumnType("double");

                    b.Property<double?>("FreightPrice")
                        .HasColumnType("double");

                    b.Property<string>("FreightType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("FreightWeight")
                        .HasColumnType("double");

                    b.Property<double>("Profit")
                        .HasColumnType("double");

                    b.Property<double>("SellPrice")
                        .HasColumnType("double");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Taxes")
                        .HasColumnType("double");

                    b.Property<double>("TotalWeight")
                        .HasColumnType("double");

                    b.HasKey("Id");

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

                    b.Property<double>("Overload")
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

                    b.Property<string>("PJ")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("costumers");
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

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("users");
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
