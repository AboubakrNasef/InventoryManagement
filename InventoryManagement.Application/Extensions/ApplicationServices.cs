using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<TimeProvider>(TimeProvider.System);
            services.AddMediator((options) =>
            {
                options.Assemblies = [typeof(ApplicationServices)];

            });
        }
    }
}
