using Market.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Market.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Market Api", Version = "v1"}); });

            var typeOfContent = typeof(Startup);

            services.AddDbContext<PostgresContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("MarketDB"),
                    b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
                )
            );

            services.AddControllers();

            services.AddScoped<IDatabaseContainer, DatabaseContainer>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostgresContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market Api v1"));
            }

            app.UseRouting();

            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}