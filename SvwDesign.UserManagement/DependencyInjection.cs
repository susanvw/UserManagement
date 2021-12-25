using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SvwDesign.UserManagement
{

    public static class DependencyInjection
    {
        public static void UserManagementModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<UserManagementOptions>(configuration.GetSection("UserManagement"));

            // get keys
            var secretKey = configuration.GetSection("UserManagement:SecretKey");
            var validateIssuer = configuration.GetSection("UserManagement:ValidateIssuer");
            var issuer = configuration.GetSection("UserManagement:ValidIssuer");
            var validateAudience = configuration.GetSection("UserManagement:ValidateAudience");
            var audience = configuration.GetSection("UserManagement:ValidAudience");
            var connectionString = configuration.GetSection("UserManagement:ConnectionstringName");

            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(
                      configuration.GetConnectionString(connectionString.Value),
                      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


            // add identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IIdentityService, IdentityService>();

            var tokenParams = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.Value)),
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true
            };

            if (validateAudience.Value.Length > 0 && bool.Parse(validateAudience.Value))
            {
                tokenParams.ValidateAudience = true;
                tokenParams.ValidAudience = audience.Value;
            }
            else
            {
                tokenParams.ValidateAudience = false;
            }

            if (validateIssuer.Value.Length > 0 && bool.Parse(validateIssuer.Value))
            {
                tokenParams.ValidateIssuer = true;
                tokenParams.ValidIssuer = issuer.Value;
            }
            else
            {
                tokenParams.ValidateIssuer = false;
            }



            services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = true;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = tokenParams;
                });
        }
    }
}
