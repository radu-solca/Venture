using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
