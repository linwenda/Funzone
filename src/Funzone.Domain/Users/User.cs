using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users.Events;

namespace Funzone.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }

        private DateTime _registrationTime;
        private EmailAddress _emailAddress;
        private string _passwordHash;
        private string _passwordSalt;
        private string _nickName;
        private bool _isActive;

        private User()
        {
        }

        private User(
            IUserChecker userChecker,
            EmailAddress emailAddress,
            string passwordHash,
            string passwordSalt)
        {
            if (!userChecker.IsUnique(emailAddress))
            {
                throw new UserDomainException("User with this email already exists.");
            }

            Id = new UserId(Guid.NewGuid());

            _registrationTime = DateTime.UtcNow;
            _emailAddress = emailAddress;
            _passwordHash = passwordHash;
            _passwordSalt = passwordSalt;
            _isActive = true;
            
            AddDomainEvent(new UserRegisteredDomainEvent(Id,_emailAddress));
        }

        public static User RegisterByEmail(
            IUserChecker userChecker,
            EmailAddress email,
            string passwordHash,
            string passwordSalt)
        {
            return new User(
                userChecker,
                email, 
                passwordHash, 
                passwordSalt);
        }

        public void EditProfile(string nickName)
        {
            _nickName = nickName;
        }
    }
}