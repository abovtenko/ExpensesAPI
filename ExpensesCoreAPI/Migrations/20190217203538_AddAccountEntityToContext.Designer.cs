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
    [Migration("20190217203538_AddAccountEntityToContext")]
    partial class AddAccountEntityToContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpensesCoreAPI.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance");

                    b.Property<DateTime?>("DateClosed");

                    b.Property<DateTime?>("DateOpened");

                    b.Property<string>("Provider");

                    b.Property<string>("Type");

                    b.Property<int>("UserID");

                    b.HasKey("AccountID");

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID");

                    b.Property<double?>("CreditAmount");

                    b.Property<double?>("DebitAmount");

                    b.Property<string>("Description");

                    b.Property<string>("TransactionDate");

                    b.HasKey("TransactionID");

                    b.HasIndex("AccountID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.Account", b =>
                {
                    b.HasOne("ExpensesCoreAPI.Models.User", "AccountUser")
                        .WithMany("Accounts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExpensesCoreAPI.Models.Transaction", b =>
                {
                    b.HasOne("ExpensesCoreAPI.Models.Account", "TransactionAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
