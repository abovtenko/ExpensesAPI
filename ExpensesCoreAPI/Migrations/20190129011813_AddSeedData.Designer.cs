﻿// <auto-generated />
using System;
using ExpensesCoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpensesCoreAPI.Migrations
{
    [DbContext(typeof(ExpensesContext))]
    [Migration("20190129011813_AddSeedData")]
    partial class AddSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpensesCoreAPI.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("CreditAmount");

                    b.Property<double?>("DebitAmount");

                    b.Property<string>("Description");

                    b.Property<string>("TransactionDate");

                    b.Property<int>("UserID");

                    b.HasKey("TransactionID");

                    b.HasIndex("UserID");

                    b.ToTable("Transactions");

                    b.HasData(
                        new { TransactionID = 1, CreditAmount = 0.0, DebitAmount = 34.5, Description = "misc", TransactionDate = "2019-01-01", UserID = 1 }
                    );
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserID = 1, Username = "UserAlpha" }
                    );
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.Transaction", b =>
                {
                    b.HasOne("ExpensesCoreAPI.Models.User", "TransactionUser")
                        .WithMany("UserTransactions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
