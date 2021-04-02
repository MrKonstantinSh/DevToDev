using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevToDev.Infrastructure.Persistence.Configurations.Identity
{
    public class RevokedTokenConfiguration : IEntityTypeConfiguration<RevokedToken>
    {
        public void Configure(EntityTypeBuilder<RevokedToken> builder)
        {
            builder.Property(rt => rt.AccessToken)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(rt => rt.RevocationDate)
                .IsRequired();
        }
    }
}