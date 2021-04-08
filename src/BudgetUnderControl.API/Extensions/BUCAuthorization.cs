using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetUnderControl.API.Extensions
{
    internal static  class BUCAuthorization
    {

        public static IServiceCollection AddBUCAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(UsersPolicy.AllUsers, policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(UserRole.User.GetStringValue(), UserRole.LimitedUser.GetStringValue(), UserRole.PremiumUser.GetStringValue(), UserRole.Admin.GetStringValue());
                });

                options.AddPolicy(UsersPolicy.Admins, policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(UserRole.Admin.GetStringValue());
                });
            });
            return services;
        }
    }
}
