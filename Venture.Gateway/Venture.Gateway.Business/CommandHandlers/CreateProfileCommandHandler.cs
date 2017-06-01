using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Business.CommandHandlers
{
    public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly IBusClient _bus;

        public CreateProfileCommandHandler(IBusClient bus)
        {
            _bus = bus;
        }

        public void ExecuteAsync(CreateProfileCommand command)
        {
            _bus.PublishAsync(
                command,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture.Commands"));
                    config.WithRoutingKey(typeof(CreateProfileCommand).Name);
                });
        }
    }
}
