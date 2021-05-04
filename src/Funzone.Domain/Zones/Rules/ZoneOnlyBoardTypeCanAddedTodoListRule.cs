using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneOnlyBoardTypeCanAddedTodoListRule : IBusinessRule
    {
        private readonly ZoneType _type;

        public ZoneOnlyBoardTypeCanAddedTodoListRule(ZoneType type)
        {
            _type = type;
        }

        public bool IsBroken()
        {
            return _type != ZoneType.Board;
        }

        public string Message => "Only board can add to-do list.";
    }
}