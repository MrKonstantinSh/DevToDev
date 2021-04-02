using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevToDev.Infrastructure.Persistence.Configurations.Identity
{
    public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
    {
        public void Configure(EntityTypeBuilder<RefreshSession> builder)
        {
            builder.Property(rs => rs.RefreshToken)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(rs => rs.Fingerprint)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(rs => rs.UserAgent)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(rs => rs.IpAddress)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(rs => rs.CreatedAt)
                .IsRequired();

            builder.Property(rs => rs.ExpiresIn)
                .IsRequired();
        }
    }
}