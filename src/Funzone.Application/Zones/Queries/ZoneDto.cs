using System;

namespace Funzone.Application.Zones.Queries
{
    public class ZoneDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid CreatorId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public int Visibility { get; set; }
    }
}