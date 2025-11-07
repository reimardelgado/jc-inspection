using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.EfConfigurations;

public class UserEfConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Email)
            .IsUnique();
    }
}