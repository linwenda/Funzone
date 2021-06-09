using System;
using System.Collections.Generic;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users.Events;

namespace Funzone.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }

        public DateTime RegistrationTime { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public string NickName { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<UserRole> _roles;
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();


        private User()
        {
        }

        private User(
            EmailAddress emailAddress,
            string passwordHash,
            string passwordSalt)
        {
            Id = new UserId(Guid.NewGuid());

            RegistrationTime = DateTime.UtcNow;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            IsActive = true;

            _roles.Add(UserRole.Guest);

            AddDomainEvent(new UserRegisteredDomainEvent(Id, EmailAddress));
        }

        public static User RegisterByEmail(
            IUserChecker userChecker,
            EmailAddress email,
            string passwordHash,
            string passwordSalt)
        {
            if (!userChecker.IsUnique(email)) throw new UserDomainException("User with this email already exists.");


            return new User(
                email,
                passwordHash,
                passwordSalt);
        }

        public void EditProfile(string nickName)
        {
            NickName = nickName;
        }
    }
}