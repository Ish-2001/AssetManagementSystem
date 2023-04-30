using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Logging;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;

namespace AssetManagementSystem
{
    public class Startup
    {
        IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DBContext>(op => op.UseSqlServer(_configuration.GetConnectionString("Database")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
