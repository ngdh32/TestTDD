using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TestTddCore.Interfaces;
using TestTddCore.Services;

namespace TestTddCore
{
    public static class InitCore
    {
        public static void InitServiceBuilder(this IServiceCollection services)
        {
            services.AddTransient<IBookQueryService, BookQueryService>();
        }
    }
}
