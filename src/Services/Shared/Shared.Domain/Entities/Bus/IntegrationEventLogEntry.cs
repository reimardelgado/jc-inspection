using Shared.Domain.SeedWork;

namespace Shared.Domain.Entities.Bus;

public class IntegrationEventLogEntry : BaseEntity
{
    #region Constructor & properties

    public Guid AggregateId { get; private set; }
    public string Status { get; private set; } = IntegrationEventStatus.NotPublished;
    public string? EventName { get; private set; }
    public string? ClassName { get; private set; }
    public string? Content { get; private set; }
    public int TimesSent { get; private set; }

    public IntegrationEvent IntegrationEvent { get; private set; } = null!;

    protected IntegrationEventLogEntry()
    {
    }

    public IntegrationEventLogEntry(IntegrationEvent integrationEvent) : base(integrationEvent.EventId)
    {
        AggregateId = integrationEvent.EventId;
        EventName = integrationEvent.GetEventName();
        ClassName = integrationEvent.GetClassName();
        TimesSent = 0;

        integrationEvent.SetIntegrationEventLogEntryId(Id);
        IntegrationEvent = integrationEvent;

        Content = integrationEvent.GetMessage();
    }

    #endregion

    #region Public methods

    public void SetStatus(string integrationEventStatus) => Status = integrationEventStatus;

    public void IncreaseTimesSent() => TimesSent++;

    public void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;

    #endregion
}