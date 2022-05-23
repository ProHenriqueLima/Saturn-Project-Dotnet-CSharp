using SaturnApi.Api.Extensions;
using SaturnApi.Api.Services;
using SaturnApi.Api.Services.Profiles;
using SaturnApi.Business.ErrorNotifications;
using SaturnApi.Business.Interfaces;

using SaturnApi.Data.Context;
using SaturnApi.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace SaturnApi.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SaturnApiDbContext>();

            services.AddScoped<IErrorNotifier, ErrorNotifier>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IEmailSettings, EmailSettings>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<ILogExceptionRepository, LogExceptionRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();
            services.AddTransient<IEmailService, EmailService>();



























            return services;
        }
    }
}
