using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Extensions;
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

        public void Execute(CreateProfileCommand command)
        {
            _bus.Command(command);
        }
    }
}
