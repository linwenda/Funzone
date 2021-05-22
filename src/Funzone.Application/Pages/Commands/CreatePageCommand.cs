using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Pages.Commands
{
    public class CreatePageCommand : ICommand<Guid>
    {
        public Guid ZoneId { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}