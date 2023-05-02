using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
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


            // Book
            services.AddScoped<IBookRepository, BookRepository>();

            // Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            // Order
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            // OrderDetail
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICurrentTime, CurrentTime>();



            // ATTENTION: if you do migration please check file README.md
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(databaseConnection));


            services.AddMemoryCache();

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
