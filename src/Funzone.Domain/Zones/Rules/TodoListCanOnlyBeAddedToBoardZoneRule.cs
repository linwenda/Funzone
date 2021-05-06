using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class TodoListCanOnlyBeAddedToBoardZoneRule : IBusinessRule
    {
        private readonly ZoneType _type;

        public TodoListCanOnlyBeAddedToBoardZoneRule(ZoneType type)
        {
            _type = type;
        }

        public bool IsBroken()
        {
            return _type != ZoneType.Board;
        }

        public string Message => "To-do list can only be added to board.";
    }
}