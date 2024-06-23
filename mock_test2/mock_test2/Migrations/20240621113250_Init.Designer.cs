﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mock_test2.Data;

#nullable disable

namespace mock_test2.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240621113250_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("mock_test2.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("ID");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            ID = 2,
                            FirstName = "Ann",
                            LastName = "Smith"
                        },
                        new
                        {
                            ID = 3,
                            FirstName = "Jack",
                            LastName = "Taylor"
                        });
                });

            modelBuilder.Entity("mock_test2.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("ID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            ID = 2,
                            FirstName = "Ann",
                            LastName = "Smith"
                        },
                        new
                        {
                            ID = 3,
                            FirstName = "Jack",
                            LastName = "Taylor"
                        });
                });

            modelBuilder.Entity("mock_test2.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("AcceptedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FulfilledAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AcceptedAt = new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Local),
                            ClientId = 1,
                            Comments = "IBYIDY",
                            EmployeeId = 1,
                            FulfilledAt = new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ID = 2,
                            AcceptedAt = new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Local),
                            ClientId = 1,
                            Comments = "IDBD",
                            EmployeeId = 2
                        },
                        new
                        {
                            ID = 3,
                            AcceptedAt = new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Local),
                            ClientId = 2,
                            EmployeeId = 3
                        });
                });

            modelBuilder.Entity("mock_test2.Models.Order_Pastry", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PastryId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("OrderId", "PastryId");

                    b.HasIndex("PastryId");

                    b.ToTable("OrderPastries");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            PastryId = 1,
                            Amount = 42,
                            Comments = "Good"
                        },
                        new
                        {
                            OrderId = 1,
                            PastryId = 2,
                            Amount = 132
                        },
                        new
                        {
                            OrderId = 2,
                            PastryId = 1,
                            Amount = 7
                        },
                        new
                        {
                            OrderId = 3,
                            PastryId = 3,
                            Amount = 7
                        },
                        new
                        {
                            OrderId = 2,
                            PastryId = 3,
                            Amount = 7,
                            Comments = "null"
                        });
                });

            modelBuilder.Entity("mock_test2.Models.Pastry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Pastries");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "etynewy",
                            Price = 42.1m,
                            Type = "A"
                        },
                        new
                        {
                            ID = 2,
                            Name = "qetbg",
                            Price = 93.4m,
                            Type = "B"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Jweg4tytack",
                            Price = 7.7m,
                            Type = "C"
                        });
                });

            modelBuilder.Entity("mock_test2.Models.Order", b =>
                {
                    b.HasOne("mock_test2.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mock_test2.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("mock_test2.Models.Order_Pastry", b =>
                {
                    b.HasOne("mock_test2.Models.Order", "Order")
                        .WithMany("OrderPastries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mock_test2.Models.Pastry", "Pastry")
                        .WithMany("OrderPastries")
                        .HasForeignKey("PastryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pastry");
                });

            modelBuilder.Entity("mock_test2.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("mock_test2.Models.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("mock_test2.Models.Order", b =>
                {
                    b.Navigation("OrderPastries");
                });

            modelBuilder.Entity("mock_test2.Models.Pastry", b =>
                {
                    b.Navigation("OrderPastries");
                });
#pragma warning restore 612, 618
        }
    }
}