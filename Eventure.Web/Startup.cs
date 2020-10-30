using Eventure.Application.Commands;
using Eventure.Infrastructure.Mongo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eventure.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", false, reloadOnChange: true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoRepositories();
            services.AddMongoDbContext(_config.GetSection("MongoDb"));
            services.AddMediatR(typeof(OpenStoreHandler));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
