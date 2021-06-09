using Ardalis.GuardClauses;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users
{
    public class EmailAddress : ValueObject
    {
        public string Address { get; }

        public EmailAddress(string address)
        {
            Guard.Against.NullOrEmpty(address, nameof(address));
            Guard.Against.InvalidFormat(address, nameof(address),
                @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
                @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");
            Address = address;
        }

        public override string ToString()
        {
            return $"EmailAddress [address = {Address}]";
        }
    }
}