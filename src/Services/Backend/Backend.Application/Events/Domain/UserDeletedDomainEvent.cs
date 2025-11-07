namespace Backend.Application.Events.Domain
{
    public class UserDeletedDomainEvent : INotification
    {
        public User User { get; }

        public UserDeletedDomainEvent(User user)
        {
            User = user;
        }
    }
}