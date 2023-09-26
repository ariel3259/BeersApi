using BeersApi.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersApi.IntegrationTest
{
    public class TestWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((services) =>
            {
                var appContext = services.SingleOrDefault((d) =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                services.Remove(appContext);
                var dbConnection = services.SingleOrDefault((d) =>
                    d.ServiceType == typeof(DbConnection));
                services.Remove(dbConnection);

                services.AddSingleton<DbConnection>((opt) =>
                {
                    SqliteConnection con = new("DataSource=:memory:");
                    con.Open();
                    return con;
                });

                services.AddDbContext<ApplicationContext>((container, opt) =>
                {
                    DbConnection con = container.GetRequiredService<DbConnection>();
                    opt.UseSqlite(con);
                });
            });
            builder.UseEnvironment("Development");
        }
    }
}
