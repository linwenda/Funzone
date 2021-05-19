using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Pages.Commands
{
    public class CreateZoneCommand : ICommand<bool>
    {
        public string Title { get; set; }
    }
}