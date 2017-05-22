﻿using System;
using LiteGuard;
using Venture.ProfileWrite.Business.CommandHandlers;
using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandDispatcher
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

            Console.WriteLine("Create command executed");

            ICommandHandler<TCommand> handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
            handler.ExecuteAsync(command);
        }
    }
}
