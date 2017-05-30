namespace Venture.Common.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void ExecuteAsync(TCommand command);
    }
}
