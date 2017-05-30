namespace Venture.Common.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
