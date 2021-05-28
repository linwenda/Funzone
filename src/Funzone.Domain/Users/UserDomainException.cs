using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users
{
    public class UserDomainException : DomainException
    {
        public UserDomainException(string message) : base(message)
        {
        }
    }
}