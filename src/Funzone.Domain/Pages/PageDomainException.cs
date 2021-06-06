using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages
{
    public class PageDomainException : DomainException
    {
        public PageDomainException(string message) : base(ResponseCode.PageDomainException, message)
        {
        }
    }
}