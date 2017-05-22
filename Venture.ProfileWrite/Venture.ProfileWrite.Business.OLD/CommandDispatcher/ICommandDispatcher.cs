using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandDispatcher
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
