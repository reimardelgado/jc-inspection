using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Infrastructure.EntityConfigurations;

public class IntegrationEventLogEntryConfig : IEntityTypeConfiguration<IntegrationEventLogEntry>
{
    public void Configure(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Ignore(t => t.IntegrationEvent);
    }
}