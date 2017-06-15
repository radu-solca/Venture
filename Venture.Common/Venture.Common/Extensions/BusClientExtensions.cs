using System;
using System.Threading.Tasks;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Events;

namespace Venture.Common.Extensions
{
    public static class BusClientExtensions
    {
        private static readonly string _commandsExchangeName = "Venture.Commands";
        private static readonly string _commandsQueueName = "Venture.Commands";
        private static readonly string _queryExchangeName = "Venture.Queries";
        private static readonly string _queryQueueName = "Venture.Queries";

        public static void Command<TCommand>(this IBusClient bus, TCommand command)
            where TCommand : class, ICommand
        {
            bus.PublishAsync(
                command,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_commandsExchangeName));
                    config.WithRoutingKey(typeof(TCommand).Name);
                });
        }

        public static void SubscribeToCommand<TCommand>(this IBusClient bus, ICommandDispatcher commandDispatcher)
            where TCommand : class, ICommand
        {
            bus.SubscribeAsync<TCommand>(
                async (command, context) =>
                {
                    await Task.Run(() => commandDispatcher.Handle(command));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_commandsExchangeName));
                    config.WithRoutingKey(typeof(TCommand).Name);
                    config.WithQueue(queue => queue.WithName(_commandsQueueName));
                });
        }

        public static TResult Query<TQuery, TResult>(this IBusClient bus, TQuery query)
            where TQuery : class, IQuery<TResult>
        {
            return bus.RequestAsync<TQuery, TResult>(
                    query,
                    configuration: config =>
                    {
                        config.WithExchange(exchange => exchange.WithName(_queryExchangeName));
                        config.WithRoutingKey(typeof(TQuery).Name);
                    })
                .Result;
        }

        public static void SubscribeToQuery<TQuery, TResult>(this IBusClient bus, IQueryDispatcher queryDispatcher) where TQuery : class, IQuery<TResult>
        {
            bus.RespondAsync<TQuery, TResult>(
                async (query, context) =>
                {
                    return await Task.Run(() => queryDispatcher.Handle(query));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_queryExchangeName));
                    config.WithRoutingKey(typeof(TQuery).Name);
                    config.WithQueue(queue => queue.WithName(_queryQueueName));
                });
        }

        public static void PublishEvent<TDomainEvent>(this IBusClient bus, TDomainEvent domainEvent, string key)
            where TDomainEvent : class, IDomainEvent
        {
            bus.PublishAsync(
                domainEvent,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture." + key + ".Events"));
                    config.WithRoutingKey(key + ".Event");
                }
            );
        }

        public static void SubscribeToEvent<TDomainEvent>(this IBusClient bus, string key, string queueName)
            where TDomainEvent : class, IDomainEvent
        {
            bus.SubscribeAsync<TDomainEvent>(
                async (domainEvent, context) =>
                {
                    // TODO: Stop hardcoding the handling of events. Either pass handler as parameter, or create event dispatching mechanism (similar to commands/queries).
                    await Task.Run(() => Console.WriteLine(domainEvent.Type + " recieved"));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture." + key + ".Events"));
                    config.WithRoutingKey(key + ".Event");
                    config.WithQueue(queue => queue.WithName("Venture." + queueName));
                });
        }
    }
}
