using System;
using Venture.Common.Cqrs.Commands;
using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        public void Execute(CreateProfileCommand command)
        {
            Console.WriteLine(" !!!! Created profile !!!! ");
        }
    }
}
