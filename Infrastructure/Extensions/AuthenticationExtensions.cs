namespace xgp_photo_api.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtOptions>(config.GetSection("Jwt"));
            services.Configure<List<AuthClient>>(config.GetSection("AuthClients"));

            services.AddScoped<IAuthClientValidator, AuthClientValidator>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
