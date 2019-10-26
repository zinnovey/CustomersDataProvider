using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        #region IEntityTypeConfiguration
        
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Customers");

            builder.Property(e => e.Id)
                .HasColumnName("CustomerID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("CustomerName");

            builder.Property(e => e.ContactEmail)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("CustomerContactEmail");

            builder.Property(e => e.MobileNumber)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CustomerMobileNumber");
            
        }

        #endregion

    }
}
