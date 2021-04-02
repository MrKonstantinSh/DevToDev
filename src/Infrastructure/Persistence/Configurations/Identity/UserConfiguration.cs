using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevToDev.Infrastructure.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(u => u.RegistrationDate)
                .IsRequired();

            builder.Property(u => u.EmailVerificationToken)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}