using Nancy;
using Venture.Users.Data;

namespace Venture.Users.WebApi
{
    public sealed class UsersModule : NancyModule
    {
        private readonly IRepository<User> _userRepository;
        public UsersModule(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            Get("/users", _ => userRepository.GetAll());
            Get("/users/{id}", parameters => userRepository.GetById(parameters.id));
        }
    }
}
