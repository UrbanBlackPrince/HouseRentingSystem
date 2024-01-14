using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HouseRentingSystem.Infractructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationService(this IServiceCollection services, Type serviceType)
        {
            Assembly serviceassembly = Assembly.GetAssembly(serviceType);
            if (serviceassembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided");
            }
            Type[] serviceTypes = serviceassembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType
                    .GetInterface($"{implementationType.Name}");

                if (interfaceType == null)
                {
                    throw new InvalidCastException($"No interface is provided for the service with name:{implementationType.Name}");
                }

                services.AddScoped(interfaceType, implementationType);
            }
            services.AddScoped<IHouseService, HouseService>();
        }
    }
}
