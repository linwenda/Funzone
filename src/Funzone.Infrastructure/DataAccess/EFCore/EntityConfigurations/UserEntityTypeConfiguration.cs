using System;
using Funzone.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funzone.Infrastructure.DataAccess.EFCore.EntityConfigurations
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property<DateTime>("_registrationTime").HasColumnName("RegistrationTime");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_passwordHash").HasColumnName("PasswordHash");
            builder.Property<string>("_passwordSalt").HasColumnName("PasswordHash");
            builder.Property<string>("_nickName").HasColumnName("NickName");
            builder.Property<bool>("_isActive").HasColumnName("IsActive");

            builder.OwnsOne<EmailAddress>("_email", e =>
            {
                e.Property(eb => eb.Address).HasColumnName("EmailAddress");
            });

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("UserRoles", "users");
                b.Property<UserId>("UserId");
                b.Property<string>("Value").HasColumnName("RoleCode");
                b.HasKey("UserId", "Value");
            });
        }
    }
}