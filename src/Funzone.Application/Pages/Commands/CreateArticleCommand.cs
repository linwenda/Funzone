using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Pages.Commands
{
    public class CreateArticleCommand : ICommand<bool>
    {
        public Guid PageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}