using CustomersDataProvider.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomersDataProvider.DataAccessLayer.Configuration
{
    sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        #region IEntityTypeConfiguration

        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Transactions");

            builder.Property(e => e.Id)
                .HasColumnName("TransactionID");

            builder.Property(e => e.CustomerId)
                .HasColumnName("CustomerID");

            builder.Property(e => e.DateTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("TransactionDateTime");

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("TransactionAmount");

            builder.Property(e => e.Currency)
                .HasColumnType("int")
                .HasColumnName("TransactionCurrency");

            builder.Property(e => e.Status)
                .HasColumnType("int")
                .HasColumnName("TransactionStatus");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_Transactions");
        }

        #endregion
        
    }
}
