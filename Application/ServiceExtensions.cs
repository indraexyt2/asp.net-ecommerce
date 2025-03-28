﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Common.Behaviors;
using FluentValidation;

namespace Application
{
    public static class ServiceExtensions 
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
