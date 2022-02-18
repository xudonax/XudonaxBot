using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace XudonaxBot.Bot.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAllImplementationsOf<T>(this IServiceCollection services, params Assembly[] assemblies) => RegisterAllImplementationsOf<T>(services, ServiceLifetime.Transient, assemblies);

        public static IServiceCollection RegisterAllImplementationsOf<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient, params Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));

            return services;
        }
    }
}
