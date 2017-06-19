using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public sealed class CreateProjectCommand : ICommand
    {
        public CreateProjectCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; }
        public string Description { get; }
    }
}
