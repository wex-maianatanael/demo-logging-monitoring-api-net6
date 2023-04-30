using Demo.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Configuration
{
    public static class DatabaseConfig
    {
        public static WebApplicationBuilder AddDatabaseConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BankDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}
