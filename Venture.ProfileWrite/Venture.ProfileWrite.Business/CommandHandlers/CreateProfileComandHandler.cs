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
            Console.WriteLine(command.Email);
            Console.WriteLine(command.FirstName);
            Console.WriteLine(command.LastName);
        }
    }
}
