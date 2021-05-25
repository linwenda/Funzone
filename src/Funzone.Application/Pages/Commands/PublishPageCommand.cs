using System;
using Funzone.Application.Configuration.Commands;
using MediatR;

namespace Funzone.Application.Pages.Commands
{
    public class PublishPageCommand : ICommand<Unit>
    {
        public Guid PageId { get; set; }
    }
}