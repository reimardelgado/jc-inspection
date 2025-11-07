namespace Backend.Application.Events.Domain
{
    public class UserCreatedDomainEvent : INotification
    {
        public User User { get; }

        public UserCreatedDomainEvent(User user)
        {
            User = user;
        }
    }
}