using EPubBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using SoloX.BlazorJsBlob;

namespace EPubBlazor;

/// <summary>
/// Extension methods to setup the EPubBlazor services.
/// </summary>
public static partial class ServiceCollectionExtensions
{

    /// <summary>
    /// Add EPubBlazor services.
    /// </summary>
    /// <param name="services">The service collection to setup.</param>
    /// <param name="serviceLifetime">Service Lifetime to use to register the Services. (Default is Scoped)</param>
    /// <returns>The given service collection updated with the EPubBlazor services.</returns>
    public static IServiceCollection AddEPubBlazor(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        => services.AddEPubBlazor(_ => { }, serviceLifetime);

    /// <summary>
    /// Add EPubBlazor services.
    /// </summary>
    /// <param name="services">The service collection to setup.</param>
    /// <param name="optionsBuilder">Options builder action delegate.</param>
    /// <param name="serviceLifetime">Service Lifetime to use to register the Services. (Default is Scoped)</param>
    /// <returns>The given service collection updated with the EPubBlazor services.</returns>
    public static IServiceCollection AddEPubBlazor(this IServiceCollection services, Action<EPubBlazorOptions> optionsBuilder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddJsBlob(serviceLifetime);
        services.AddHttpClient();
        // services.AddSingleton<IBufferService, BufferService>();
        services.AddScoped<EPubJsInterop>();

        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<BlobManagerService>();
                services.AddSingleton<EPubNavigationService>();
                services.AddSingleton<EPubSettingsService>();
                services.AddSingleton<EPubThemeService>();
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<BlobManagerService>();
                services.AddScoped<EPubNavigationService>();
                services.AddScoped<EPubSettingsService>();
                services.AddScoped<EPubThemeService>();
                break;
            case ServiceLifetime.Transient:
            default:
                services.AddTransient<BlobManagerService>();
                services.AddTransient<EPubNavigationService>();
                services.AddTransient<EPubSettingsService>();
                services.AddTransient<EPubThemeService>();
                break;
        }

        services.Configure(optionsBuilder);

        return services;
    }
}