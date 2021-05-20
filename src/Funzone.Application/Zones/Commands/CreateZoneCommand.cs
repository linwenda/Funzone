using System;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.SharedKernel;

namespace Funzone.Application.Zones.Commands
{
    public class CreateZoneCommand : ICommand<Guid>
    {
        public string Title { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public Visibility Visibility { get; set; }
    }
}