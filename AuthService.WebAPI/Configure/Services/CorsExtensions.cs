using Microsoft.IdentityModel.Protocols;

namespace AuthService.WebAPI.Configure.Services;

public static class CorsExtensions
{
    public static IServiceCollection AddAllowCors(this IServiceCollection services, ConfigurationManager configuration)
    {
        string[] allowedOrigins = configuration.GetSection("AllowedHosts").Value?.Split(';') ?? new string[] { "*" };
        Console.WriteLine(allowedOrigins);
        return services.AddCors(options =>
        {
            options.AddPolicy("AllowAllCors", policy =>
            {
                policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
            
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .WithMethods(new[] { "GET", "POST", "PUT", "DELETE"})
                    .AllowAnyHeader();
            });
        });
    } 
}