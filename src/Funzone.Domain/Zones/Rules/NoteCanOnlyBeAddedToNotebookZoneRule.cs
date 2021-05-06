using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class NoteCanOnlyBeAddedToNotebookZoneRule : IBusinessRule
    {
        private readonly ZoneType _type;

        public NoteCanOnlyBeAddedToNotebookZoneRule(ZoneType type)
        {
            _type = type;
        }

        public bool IsBroken()
        {
            return _type != ZoneType.Notebook;
        }

        public string Message => "Note can only be added to notebook.";
    }
}