using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using BPLogix.BooksCvsGenerator.Infrastructure.Http;
using BPLogix.BooksCvsGenerator.Infrastructure.Manager;
using BPLogix.BooksCvsGenerator.Infrastructure.Providers;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BPLogix.BooksCvsGenerator.Infrastructure
{
    public static class DependencyInjectionConfiguration
    {
        public static Assembly Instance = typeof(DependencyInjectionConfiguration).Assembly;
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatRConfiguration(configuration)
                    .AddInfrastructureConfiguration(configuration)
                    .AddHttpClientConfiguration(configuration);
            return services;
        }

        public static IServiceCollection AddFastEndpointsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "My API";
                    s.Version = "v1";
                };
            });
            return services;
        }

        private static IServiceCollection AddMediatRConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Instance);
            return services;
        }

        private static IServiceCollection AddHttpClientConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IBooksApi, BooksHttpClient>("bookClient")
                    .ConfigureHttpClient((sp, httpClient) =>
                    {
                        var cacheProvider = sp.GetService<ICacheProvider>();
                        httpClient.BaseAddress = new Uri(configuration["BookApiConfiguration:BaseUrl"] ?? string.Empty);
                    });

            return services;
        }

        private static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<ICsvManager, CsvGenerator>();
            return services;
        }
    }
}
