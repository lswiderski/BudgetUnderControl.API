﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application
{
    public static class Extenstions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            return services;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
