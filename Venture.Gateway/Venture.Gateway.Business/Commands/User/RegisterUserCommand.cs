using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public RegisterUserCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; }
        public string Password { get; }
    }
}
