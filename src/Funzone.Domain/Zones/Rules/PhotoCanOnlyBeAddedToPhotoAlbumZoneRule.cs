using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class PhotoCanOnlyBeAddedToPhotoAlbumZoneRule : IBusinessRule
    {
        private readonly ZoneType _type;

        public PhotoCanOnlyBeAddedToPhotoAlbumZoneRule(ZoneType type)
        {
            _type = type;
        }

        public bool IsBroken()
        {
            return _type != ZoneType.Notebook;
        }

        public string Message => "Photo can only be added to photo-album.";
    }
}