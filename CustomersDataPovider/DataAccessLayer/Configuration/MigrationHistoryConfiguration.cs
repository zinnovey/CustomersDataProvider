using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    class MigrationHistoryConfiguration : IEntityTypeConfiguration<MigrationHistory>
    {
        #region IEntityTypeConfiguration members

        public void Configure(EntityTypeBuilder<MigrationHistory> builder)
        {
            builder.HasKey(e => new { e.MigrationId, e.ContextKey })
                .HasName("PK_dbo.__MigrationHistory");

            builder.ToTable("__MigrationHistory");

            builder.Property(e => e.MigrationId)
                .HasMaxLength(150);

            builder.Property(e => e.ContextKey)
                .HasMaxLength(300);

            builder.Property(e => e.Model)
                .IsRequired();

            builder.Property(e => e.ProductVersion)
                .IsRequired()
                .HasMaxLength(32);
        }

        #endregion

    }
}
