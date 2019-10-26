using System.Diagnostics.CodeAnalysis;
using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CustomersDBContext : DbContext
    {
        #region Properties

        public virtual DbSet<CustomerEntity> Customers { get; set; }

        public virtual DbSet<TransactionEntity> Transactions { get; set; }

        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        #endregion

        #region Constructors

        public CustomersDBContext()
        {
        }

        public CustomersDBContext(DbContextOptions<CustomersDBContext> options)
            : base(options)
        {
        }

        #endregion

        #region Protected

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO: move to config
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
