using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public class ZoneType : ValueObject
    {
        public static ZoneType Board = new ZoneType(nameof(Board));
        public static ZoneType PhotoAlbum = new ZoneType(nameof(PhotoAlbum));
        public static ZoneType Untitled = new ZoneType(nameof(Untitled));
        public static ZoneType Timeline = new ZoneType(nameof(Timeline));

        public string Value { get; }

        private ZoneType(string value)
        {
            Value = value;
        }
    }
}