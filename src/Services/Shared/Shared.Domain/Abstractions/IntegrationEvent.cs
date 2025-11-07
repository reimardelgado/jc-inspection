using System.Text.Json;

namespace Shared.Domain.Abstractions;

public abstract class IntegrationEvent
{
    public string AggregateId { get; set; }
    public Guid EventId { get; set; }

    public DateTime CreatedAtDate { get; }
    public Guid IntegrationEventLogEntryId { get; private set; }

    public abstract string GetEventName();

    protected IntegrationEvent()
    {
        AggregateId = Guid.NewGuid().ToString();
        EventId = Guid.NewGuid();
        CreatedAtDate = DateTime.UtcNow;
    }

    protected IntegrationEvent(string aggregateId)
    {
        AggregateId = aggregateId;
        EventId = Guid.NewGuid();
        CreatedAtDate = DateTime.UtcNow;
    }

    public string GetMessage()
    {
        return JsonSerializer.Serialize(this);
    }

    public string GetClassName()
    {
        return GetType().Name;
    }

    public void SetIntegrationEventLogEntryId(Guid id)
    {
        IntegrationEventLogEntryId = id;
    }
}