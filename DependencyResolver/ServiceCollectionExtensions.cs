using Core.Interfaces.Services;
using Core.Services;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependenciesResolver
{
    public static class ServiceCollectionExtensions
    {

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration["Data:SportStoreProducts:ConnectionString"];

            services.AddSingleton<Infrastructure.Interfaces.IMapper, SportStoreAutoMapper>();

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));
        }
    }
}
