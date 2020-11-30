using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // telling static to return iservicecollection , always 'this' before class that we are extending
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
         {


            services.AddScoped<ITokenService, TokenService>(); //scoping token to the lifetime of httprequest
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //options is parametr that we pass to statement block inside {}
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
         }
        
    }
}