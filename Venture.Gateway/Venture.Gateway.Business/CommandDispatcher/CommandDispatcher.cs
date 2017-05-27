using System.Threading.Tasks;
using LiteGuard;
using RawRabbit;
using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Business.CommandDispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IBusClient _bus;

        public CommandDispatcher(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            Guard.AgainstNullArgument(nameof(command), command);

            await _bus.PublishAsync(command);
        }
    }
}
