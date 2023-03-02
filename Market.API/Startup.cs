using Market.API.Database;
using Microsoft.EntityFrameworkCore;

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
            
            var typeOfContent = typeof(Startup);
            services.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MarketDB"),
                    b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)));
            

            services.AddControllers();

            services.AddScoped<IDatabaseContainer, DatabaseContainer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostgresContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}