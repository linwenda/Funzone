namespace Funzone.Domain.Pages
{
    public record PageStatus
    {
        public static PageStatus Draft => new PageStatus(nameof(Draft));
        public static PageStatus Published = new PageStatus(nameof(Published));
        public static PageStatus Closed = new PageStatus(nameof(Closed));
        
        public string Value { get; }
        
        private PageStatus(string value)
        {
            Value = value;
        }

        public static PageStatus Of(string status)
        {
            return new PageStatus(status);
        }
    }
}