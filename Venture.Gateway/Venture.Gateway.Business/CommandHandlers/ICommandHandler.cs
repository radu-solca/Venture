using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Business.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
