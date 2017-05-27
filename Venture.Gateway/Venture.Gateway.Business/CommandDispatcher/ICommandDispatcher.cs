using System.Threading.Tasks;
using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Business.CommandDispatcher
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
