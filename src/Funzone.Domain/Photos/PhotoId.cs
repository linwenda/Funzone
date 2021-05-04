using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Photos
{
    public class PhotoId : TypedIdValueBase
    {
        public PhotoId(Guid value) : base(value)
        {
        }
    }
}