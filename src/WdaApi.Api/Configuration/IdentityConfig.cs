using WdaApi.Api.Extensions;
using WdaApi.Business.Models;
using WdaApi.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WdaApi.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {    

            services.AddDefaultIdentity<ApplicationUser>
                (options =>
                {
                   // options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                    
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SaturnApiDbContext>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddErrorDescriber<IdentityTranslatedMessages>()
                .AddDefaultTokenProviders();

            //services.Configure<DataProtectionTokenProviderOptions>(opt => 
            //    opt.TokenLifespan = TimeSpan.FromHours(2));

            //JWT
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //é importante exigir o HTTPS
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    //permitindo todos os dominios
                    ValidateAudience = false,
                    //ValidAudience = appSettings.ValidIn,
                    ValidIssuer = appSettings.Emitter
                };
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            using (var serviceScoped = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScoped.ServiceProvider.GetRequiredService<SaturnApiDbContext>();

                context.Database.Migrate();
            }

            return app;
        }
    }
}
