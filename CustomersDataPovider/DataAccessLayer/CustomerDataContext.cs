﻿using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CustomerDataContext : DbContext
    {
        #region Properties

        public virtual DbSet<CustomerEntity> Customers { get; set; }

        public virtual DbSet<TransactionEntity> Transactions { get; set; }

        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        #endregion

        #region Constructors

        public CustomerDataContext()
        {
        }

        public CustomerDataContext(DbContextOptions<CustomerDataContext> options)
            : base(options)
        {
        }

        #endregion

        #region Protected

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-B4S3HSP\\SQLEXPRESS;Initial Catalog=Customers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MigrationHistoryConfiguration());
        }

        #endregion

    }
}
