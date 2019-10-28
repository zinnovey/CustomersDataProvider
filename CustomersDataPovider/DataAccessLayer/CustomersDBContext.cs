using System;
using System.Diagnostics.CodeAnalysis;
using CustomersDataProvider.DataAccessLayer.Configuration;
using CustomersDataProvider.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomersDataProvider.DataAccessLayer
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CustomersDBContext : DbContext
    {
        #region Constants

        private const String ConnectionStringName = "CustomersDB";
        private const String DefaultConnectionString = "Data Source=DESKTOP-B4S3HSP\\SQLEXPRESS;Initial Catalog=Customers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #endregion    

        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        public virtual DbSet<CustomerEntity> Customers { get; set; }

        public virtual DbSet<TransactionEntity> Transactions { get; set; }

        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        #endregion

        #region Constructors

        public CustomersDBContext(IConfiguration configuration) 
            => _configuration = configuration;

        public CustomersDBContext(DbContextOptions<CustomersDBContext> options, IConfiguration configuration)
            : base(options) =>
            _configuration = configuration;

        #endregion

        #region Protected

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration?.GetConnectionString(ConnectionStringName) 
                                            ?? DefaultConnectionString);
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
