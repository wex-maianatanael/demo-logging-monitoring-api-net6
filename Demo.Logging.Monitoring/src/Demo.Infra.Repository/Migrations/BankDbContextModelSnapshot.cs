﻿// <auto-generated />
using System;
using Demo.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.Infra.Repository.Migrations
{
    [DbContext(typeof(BankDbContext))]
    partial class BankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Demo.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal");

                    b.Property<Guid>("BankID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            ID = new Guid("fc3b2cfc-e334-4b3b-9cf9-65bcfdc31c17"),
                            Active = true,
                            Balance = 1000m,
                            BankID = new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"),
                            CreatedDate = new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8574),
                            CustomerID = new Guid("f9748ff6-195f-4279-830b-1d1576fbf87c"),
                            Number = 111111
                        },
                        new
                        {
                            ID = new Guid("4be52ed1-d294-49f8-8423-710c5d66be79"),
                            Active = true,
                            Balance = 1200m,
                            BankID = new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"),
                            CreatedDate = new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8596),
                            CustomerID = new Guid("a355cea6-ecb7-491e-857e-e115fc57ff95"),
                            Number = 222222
                        },
                        new
                        {
                            ID = new Guid("47a864d4-04de-444b-9ff5-fa6e4cbb73cd"),
                            Active = true,
                            Balance = 2300m,
                            BankID = new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"),
                            CreatedDate = new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8620),
                            CustomerID = new Guid("4203b06b-f962-4e59-b848-a43a2cceb6f4"),
                            Number = 333333
                        });
                });

            modelBuilder.Entity("Demo.Domain.Entities.Bank", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            ID = new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"),
                            Name = "Bank 1"
                        });
                });

            modelBuilder.Entity("Demo.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = new Guid("f9748ff6-195f-4279-830b-1d1576fbf87c"),
                            BirthDate = new DateTime(1986, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Kyle"
                        },
                        new
                        {
                            ID = new Guid("a355cea6-ecb7-491e-857e-e115fc57ff95"),
                            BirthDate = new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Alice"
                        },
                        new
                        {
                            ID = new Guid("4203b06b-f962-4e59-b848-a43a2cceb6f4"),
                            BirthDate = new DateTime(2000, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bob"
                        });
                });

            modelBuilder.Entity("Demo.Domain.Entities.Account", b =>
                {
                    b.HasOne("Demo.Domain.Entities.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Domain.Entities.Customer", "Customer")
                        .WithOne("Account")
                        .HasForeignKey("Demo.Domain.Entities.Account", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}