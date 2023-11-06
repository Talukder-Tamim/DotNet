using Ecommerce.Database;
using Ecommerce.Repositories;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Services.Abstructions.Customers;
using Ecommerce.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Configurations
{
    public static class DependencyConfigurations
    {
        public static void Configure(IServiceCollection services) 
        {
            services.AddTransient<ICustomerRepository>(service =>
            {
                var db = service.GetService<ApplicationDbContext>();
                return new CustomerRepository(db);
            });

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddHttpClient();

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                string connectionString = "Server=IT-TAMIM; Database=SampleCommerceDB; Trusted_Connection=True; TrustServerCertificate=True;";
                option.UseSqlServer(connectionString);
            });
        }
    }
}
