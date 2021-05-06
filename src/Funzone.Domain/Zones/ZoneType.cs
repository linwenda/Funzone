using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public class ZoneType : ValueObject
    {
        public static ZoneType Board = new ZoneType(nameof(Board));
        public static ZoneType Notebook = new ZoneType(nameof(Notebook));
        public static ZoneType PhotoAlbum = new ZoneType(nameof(PhotoAlbum));

        public string Value { get; }

        private ZoneType(string value)
        {
            Value = value;
        }
    }
}