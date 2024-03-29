﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SelfFinanceAPI.Core;

#nullable disable

namespace SelfFinanceAPI.Core.Migrations
{
    [DbContext(typeof(SelfFinanceDbContext))]
    [Migration("20230823150045_InsertData")]
    partial class InsertData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SelfFinanceAPI.Core.Models.ExpenseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsIncome")
                        .HasColumnType("bit")
                        .HasColumnName("IsIncome");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ExpeseTypes");
                });

            modelBuilder.Entity("SelfFinanceAPI.Core.Models.FinancialOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateTime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("TypeId");

                    b.HasKey("Id");

                    b.ToTable("FinancialOperations");
                });
#pragma warning restore 612, 618
        }
    }
}
