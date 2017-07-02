using System;
using System.Linq;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.Users.Data;

namespace Venture.Users.Application
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IRepository<User> _useRepository;

        public RegisterUserCommandHandler(IRepository<User> useRepository)
        {
            _useRepository = useRepository;
        }

        public void Handle(RegisterUserCommand command)
        {
            var userWithSameName = Enumerable.FirstOrDefault<User>(_useRepository.Get(), u => u.UserName == command.Name);
            if (userWithSameName != null)
            {
                return;
            }

            //var newUser = new User { Id = Guid.NewGuid(), Name = command.Name, Password = command.Password };
            //_useRepository.Add(newUser);
        }
    }
}