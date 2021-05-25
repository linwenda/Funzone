using System;
using Funzone.Application.Configuration.Commands;
using MediatR;

namespace Funzone.Application.Pages.Commands
{
    public class SavePageDraftCommand : ICommand<Unit>
    {
        public Guid PageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}