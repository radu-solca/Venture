﻿using System;
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
        private const string QueryExchangeName = "Venture.Queries";
        private const string EventsExchangeName = "Venture.EventFeed";

        private static string _serviceName;

        public static void SetServiceName(string serviceName)
        {
            _serviceName = serviceName;
        }

        public static void PublishCommand<TCommand>(this IBusClient bus, TCommand command)
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
                    await Task.Run(() => commandHandler.Handle(command));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(CommandsExchangeName));
                    config.WithRoutingKey(typeof(TCommand).Name);
                    config.WithQueue(queue => queue.WithName(CommandsExchangeName + "." + typeof(TCommand).Name + ".Queue"));
                });
        }

        public static TResult PublishQuery<TQuery, TResult>(this IBusClient bus, TQuery query)
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
                    return await Task.Run(() => queryHandler.Handle(query));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(QueryExchangeName));
                    config.WithRoutingKey(typeof(TQuery).Name);
                    config.WithQueue(queue => queue.WithName(QueryExchangeName + "." + typeof(TQuery).Name + ".Queue"));
                });
        }

        public static void PublishEvent<TEvent>(this IBusClient bus, TEvent domainEvent)
            where TEvent : DomainEvent
        {
            bus.PublishAsync(
                domainEvent,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName(EventsExchangeName));
                    config.WithRoutingKey(domainEvent.Type);
                }
            );
        }

        public static void SubscribeToEvent<TEvent>(this IBusClient bus, IEventHandler<TEvent> eventHandler) 
            where TEvent : DomainEvent
        {
            bus.SubscribeAsync<TEvent>(
                async (domainEvent, context) =>
                {
                    await Task.Run(() => eventHandler.Handle(domainEvent));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName(EventsExchangeName));
                    config.WithRoutingKey(typeof(TEvent).Name);
                    // Create an unique queue for each subscription, to facilitate broadcasting with multiple instances of the same service.
                    config.WithQueue(queue => queue.WithName("Venture." + _serviceName + "." + typeof(TEvent).Name + "Queue." + Guid.NewGuid()));
                });
        }
    }
}
