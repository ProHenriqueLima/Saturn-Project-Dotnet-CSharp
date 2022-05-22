using WdaApi.Api.Extensions;
using WdaApi.Api.Services;
using WdaApi.Api.Services.Profiles;
using WdaApi.Business.ErrorNotifications;
using WdaApi.Business.Interfaces;

using WdaApi.Data.Context;
using WdaApi.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace WdaApi.Api.Configuration
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
