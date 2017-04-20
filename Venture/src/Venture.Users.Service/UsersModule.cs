using Nancy;

namespace Venture.Users.Service
{
    public sealed class UsersModule : NancyModule
    {
        public UsersModule()
        {
            Get("/", _ => "Hello World");
        }
    }
}
