using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecrutmentSystem.Data;

namespace RecrutmentSystem.Infrastructures
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase( this IApplicationBuilder app)
        {
            using var scoperdServices = app.ApplicationServices.CreateScope();

            var data = scoperdServices.ServiceProvider.GetService<RecrutmentSystemDbContext>();

            data.Database.Migrate();
            return app;
        }
    }
}
