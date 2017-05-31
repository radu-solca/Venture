using System;
using LiteGuard;

namespace Venture.Common.Cqrs.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            Guard.AgainstNullArgument(nameof(serviceProvider), serviceProvider);
            _serviceProvider = serviceProvider;
        }

        public void Handle<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            Guard.AgainstNullArgument(nameof(command), command);

            ICommandHandler<TCommand> handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

            if (handler == null)
            {
                throw new Exception("Command handler not found for type " + typeof(ICommandHandler<TCommand>));
            }

            handler.Execute(command);
        }
    }
}
