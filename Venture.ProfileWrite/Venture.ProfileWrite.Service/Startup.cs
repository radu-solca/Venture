﻿using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Extensions;
using Venture.Common.Cqrs.Queries;
using Venture.ProfileWrite.Business.CommandHandlers;
using Venture.ProfileWrite.Business.Commands;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Business.QueryHandlers;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Service
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DI
            services.AddVentureCqrs();

            services.AddTransient<ICommandHandler<CreateProfileCommand>, CreateProfileComandHandler>();

            services.AddTransient<IQueryHandler<GetEventsQuery, IEnumerable<Event>>, GetEventsQueryHandler>();

            services.AddTransient<IEventStore, EventStore>();

            // Add framework services.
            services.AddMvc();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
