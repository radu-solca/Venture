using System.Reflection;
using RawRabbit;
using RawRabbit.Common;
using Venture.Gateway.Business.CommandHandlers;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.EventHandlers;
using Venture.Gateway.Business.Events;
using Venture.Gateway.Business.Queries;
using Venture.Gateway.Business.QueryHandlers;

namespace Venture.Gateway.Business.Extensions
{
    public static class RawRabbitExtensions
    {
        public static ISubscription SubscribeToCommand<TCommand>(
            this IBusClient bus,
            ICommandHandler<TCommand> handler,
            string name = null
        ) where TCommand : ICommand
        {
            return
                bus.SubscribeAsync<TCommand>(
                    async (msg, context) => await handler.ExecuteAsync(msg),
                    cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TCommand>(name)))
                );
        }

        public static ISubscription SubscribeToEvent<TDomainEvent>(
            this IBusClient bus,
            IEventHandler<TDomainEvent> handler,
            string name = null
        ) where TDomainEvent : IDomainEvent
        {
            return
                bus.SubscribeAsync<TDomainEvent>(
                    async (msg, context) => await handler.ExecuteAsync(msg),
                    cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TDomainEvent>(name)))
                );
        }

        public static ISubscription SubscribeToQuery<TQuery, TResult>(
            this IBusClient bus,
            IQueryHandler<TQuery, TResult> handler,
            string name = null
        ) where TQuery : IQuery<TResult>
        {
            return
                bus.RespondAsync<TQuery, TResult>(
                    async (msg, context) => await handler.RetrieveAsync(msg),
                    cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TQuery>(name)))
                );
        }

        private static string GetExchangeName<T>(string name = null)
        {
            return string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}"
                : $"{name}/{typeof(T).Name}";
        }
    }
}
