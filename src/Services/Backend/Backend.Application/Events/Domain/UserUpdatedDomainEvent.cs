namespace Backend.Application.Events.Domain
{
    public class UserUpdatedDomainEvent : INotification
    {
        public User User { get; }

        public UserUpdatedDomainEvent(User user)
        {
            User = user;
        }
    }
}