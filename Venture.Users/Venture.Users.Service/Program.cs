using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.Users.Application;
using Venture.Users.Data;

namespace Venture.Users.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Users");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProjectRead")

                .AddDbContext<UsersContext>(options =>
                    options.UseSqlServer(connectionString)
                )

                // Repositories
                .AddTransient<IRepository<User>, UserRepository>()

                .AddTransient<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>()
                .AddTransient<IQueryHandler<GetUserQuery>, GetUserQueryHandler>()
                .AddTransient<IQueryHandler<GetUserByNameQuery>, GetUserByNameQueryHandler>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var registerUserHandler = (ICommandHandler<RegisterUserCommand>)serviceProvider.GetService(typeof(ICommandHandler<RegisterUserCommand>));
            var getUserQueryHandler = (IQueryHandler<GetUserQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetUserQuery>));
            var getUserByNameQueryHandler = (IQueryHandler<GetUserByNameQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetUserByNameQuery>));

            bus.SubscribeToCommand(registerUserHandler);
            bus.SubscribeToQuery(getUserQueryHandler);
            bus.SubscribeToQuery(getUserByNameQueryHandler);


            Console.ReadKey();
        }
    }
}