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

        public static void SubscribeToCommand<TCommand>(this IBusClient bus, ICommandHandler<TCommand> commandHandler)
            where TCommand : class, ICommand
        {
            bus.SubscribeAsync<TCommand>(
                async (command, context) =>
                {
                    await Task.Run(() => commandHandler.Execute(command));
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

        public static void SubscribeToQuery<TQuery, TResult>(this IBusClient bus, IQueryHandler<TQuery, TResult> queryHandler) where TQuery : class, IQuery<TResult>
        {
            bus.RespondAsync<TQuery, TResult>(
                async (query, context) =>
                {
                    return await Task.Run(() => queryHandler.Retrieve(query));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_queryExchangeName));
                    config.WithRoutingKey(typeof(TQuery).Name);
                    config.WithQueue(queue => queue.WithName(_queryQueueName));
                });
        }

        public static void PublishEvent(this IBusClient bus, DomainEvent domainEvent, string key)
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

        public static void SubscribeToEvent(this IBusClient bus, string key, string queueName, Action<DomainEvent> eventHandler)
        {
            bus.SubscribeAsync<DomainEvent>(
                async (domainEvent, context) =>
                {
                    await Task.Run(() => eventHandler(domainEvent));
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
