﻿// <auto-generated />
using System;
using BudgetUnderControl.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(TransactionsContext))]
    [Migration("20210416190112_UserIdAsGuid_part2")]
    partial class UserIdAsGuid_part2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BudgetUnderControl.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsIncludedToTotal")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentAccountId")
                        .HasColumnType("int");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountGroupId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.AccountGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("AccountGroup");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.AccountSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastTransactionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PreviousAccountSnapshotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LastTransactionId");

                    b.HasIndex("PreviousAccountSnapshotId");

                    b.ToTable("AccountSnapshot");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("FullName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<short>("Number")
                        .HasColumnType("smallint");

                    b.Property<string>("Symbol")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToCurrencyId");

                    b.ToTable("ExchangeRate");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.FileToTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("TransactionId");

                    b.ToTable("FileToTransaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Icon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Icon");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Synchronization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Component")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastSyncAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Synchronization");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.TagToTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TagToTransaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("UserExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AddedById")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AddedById");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FromTransactionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToTransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromTransactionId");

                    b.HasIndex("ToTransactionId");

                    b.ToTable("Transfer");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActivatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Account", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.AccountGroup", "AccountGroup")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountGroupId")
                        .HasConstraintName("ForeignKey_Account_AccountGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Currency", "Currency")
                        .WithMany("Accounts")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("ForeignKey_Account_Currency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId");

                    b.Navigation("AccountGroup");

                    b.Navigation("Currency");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.AccountGroup", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.User", "Owner")
                        .WithMany("AccountGroups")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.AccountSnapshot", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.Account", "Account")
                        .WithMany("AccountSnapshots")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("ForeignKey_AccountSnapshot_Account")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Transaction", "LastTransaction")
                        .WithMany("AccountSnapshots")
                        .HasForeignKey("LastTransactionId")
                        .HasConstraintName("ForeignKey_AccountSnapshot_LastTransaction")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.AccountSnapshot", "PreviousAccountSnapshot")
                        .WithMany()
                        .HasForeignKey("PreviousAccountSnapshotId");

                    b.Navigation("Account");

                    b.Navigation("LastTransaction");

                    b.Navigation("PreviousAccountSnapshot");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Category", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.ExchangeRate", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.Currency", "FromCurrency")
                        .WithMany("FromExchangeRates")
                        .HasForeignKey("FromCurrencyId")
                        .HasConstraintName("ForeignKey_ExchangeRate_FromCurrency")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Currency", "ToCurrency")
                        .WithMany("ToExchangeRates")
                        .HasForeignKey("ToCurrencyId")
                        .HasConstraintName("ForeignKey_ExchangeRate_ToCurrency")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.FileToTransaction", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.File", "File")
                        .WithMany("FileToTransactions")
                        .HasForeignKey("FileId")
                        .HasConstraintName("ForeignKey_FileToTransaction_File")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Transaction", "Transaction")
                        .WithMany("FilesToTransaction")
                        .HasForeignKey("TransactionId")
                        .HasConstraintName("ForeignKey_FileToTransaction_Transaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Synchronization", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Tag", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.TagToTransaction", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.Tag", "Tag")
                        .WithMany("TagToTransactions")
                        .HasForeignKey("TagId")
                        .HasConstraintName("ForeignKey_TagToTransaction_Tag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Transaction", "Transaction")
                        .WithMany("TagsToTransaction")
                        .HasForeignKey("TransactionId")
                        .HasConstraintName("ForeignKey_TagToTransaction_Transaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Token", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("ForeignKey_Token_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Transaction", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("ForeignKey_Transaction_Account")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.User", "AddedBy")
                        .WithMany("Transactions")
                        .HasForeignKey("AddedById");

                    b.HasOne("BudgetUnderControl.Domain.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("ForeignKey_Transaction_Category");

                    b.Navigation("Account");

                    b.Navigation("AddedBy");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Transfer", b =>
                {
                    b.HasOne("BudgetUnderControl.Domain.Transaction", "FromTransaction")
                        .WithMany("FromTransfers")
                        .HasForeignKey("FromTransactionId")
                        .HasConstraintName("ForeignKey_Transfer_FromTransaction")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetUnderControl.Domain.Transaction", "ToTransaction")
                        .WithMany("ToTransfers")
                        .HasForeignKey("ToTransactionId")
                        .HasConstraintName("ForeignKey_Transfer_ToTransaction")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromTransaction");

                    b.Navigation("ToTransaction");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Account", b =>
                {
                    b.Navigation("AccountSnapshots");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.AccountGroup", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Currency", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("FromExchangeRates");

                    b.Navigation("ToExchangeRates");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.File", b =>
                {
                    b.Navigation("FileToTransactions");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Tag", b =>
                {
                    b.Navigation("TagToTransactions");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.Transaction", b =>
                {
                    b.Navigation("AccountSnapshots");

                    b.Navigation("FilesToTransaction");

                    b.Navigation("FromTransfers");

                    b.Navigation("TagsToTransaction");

                    b.Navigation("ToTransfers");
                });

            modelBuilder.Entity("BudgetUnderControl.Domain.User", b =>
                {
                    b.Navigation("AccountGroups");

                    b.Navigation("Accounts");

                    b.Navigation("Tokens");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
