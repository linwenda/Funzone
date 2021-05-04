using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Notes
{
    public class NoteId : TypedIdValueBase
    {
        public NoteId(Guid value) : base(value)
        {
        }
    }
}