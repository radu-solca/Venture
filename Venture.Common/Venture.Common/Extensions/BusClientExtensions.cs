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
        private const string CommandsExchangeName = "Venture.Commands";
        private const string CommandsQueueName = "Venture.Commands";
        private const string QueryExchangeName = "Venture.Queries";
        private const string QueryQueueName = "Venture.Queries";
        private static string _eventsExchangeName;
        private static string _eventsQueueName;
        private const string EventsKeyName = "Venture.DomainEvent";

        public static void SetAppName(string appName)
        {
            _eventsExchangeName = "Venture." + appName + ".EventFeed";
            _eventsQueueName = "Venture." + appName + "EventQueue";
        }

        public static void Command<TCommand>(this IBusClient bus, TCommand command)
            where TCommand : class, ICommand
        {
            bus.PublishAsync(
                command,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName(CommandsExchangeName));
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
                    config.WithExchange(exchange => exchange.WithName(CommandsExchangeName));
                    config.WithRoutingKey(typeof(TCommand).Name);
                    config.WithQueue(queue => queue.WithName(CommandsQueueName));
                });
        }

        public static TResult Query<TQuery, TResult>(this IBusClient bus, TQuery query)
            where TQuery : class, IQuery<TResult>
        {
            return bus.RequestAsync<TQuery, TResult>(
                    query,
                    configuration: config =>
                    {
                        config.WithExchange(exchange => exchange.WithName(QueryExchangeName));
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
                    config.WithExchange(exchange => exchange.WithName(QueryExchangeName));
                    config.WithRoutingKey(typeof(TQuery).Name);
                    config.WithQueue(queue => queue.WithName(QueryQueueName));
                });
        }

        public static void PublishEvent(this IBusClient bus, DomainEvent domainEvent)
        {
            bus.PublishAsync(
                domainEvent,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_eventsExchangeName));
                    config.WithRoutingKey(EventsKeyName);
                }
            );
        }

        public static void SubscribeToEvent(this IBusClient bus, Action<DomainEvent> eventHandler)
        {
            bus.SubscribeAsync<DomainEvent>(
                async (domainEvent, context) =>
                {
                    await Task.Run(() => eventHandler(domainEvent));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(_eventsExchangeName));
                    config.WithRoutingKey(EventsKeyName);
                    config.WithQueue(queue => queue.WithName(_eventsQueueName));
                });
        }
    }
}
