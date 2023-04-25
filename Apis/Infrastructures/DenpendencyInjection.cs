using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Domain.Interfaces;
using Domain.Services;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {

            // user
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IJWTService, JWTService>();

            services.AddScoped<IEmailService, EmailService>();

            // Book
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
         

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICurrentTime, CurrentTime>();



            // ATTENTION: if you do migration please check file README.md
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(databaseConnection));


            services.AddMemoryCache();

            // this configuration just use in-memory for fast develop
            //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
