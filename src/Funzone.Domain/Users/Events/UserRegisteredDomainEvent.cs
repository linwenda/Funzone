using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users.Events
{
    public class UserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegisteredDomainEvent(UserId userId, EmailAddress email)
        {
            UserId = userId;
            Email = email;
        }

        public UserId UserId { get; }
        public EmailAddress Email { get; }
    }
}