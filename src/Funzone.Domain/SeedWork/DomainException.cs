using System;

namespace Funzone.Domain.SeedWork
{
    public class DomainException : Exception
    {
        public ResponseCode Code { get; }

        public DomainException(ResponseCode code, string message) : base(message)
        {
        }
    }
}