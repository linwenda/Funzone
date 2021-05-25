using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages
{
    public class PageStatus : ValueObject
    {
        public string Value { get; }
        public static PageStatus Published = new PageStatus(nameof(Published));
        public static PageStatus Draft = new PageStatus(nameof(Draft));
        public static PageStatus Closed = new PageStatus(nameof(Closed));

        private PageStatus(string value)
        {
            Value = value;
        }
    }
}