using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CrossExchange
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExchangeContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IShareRepository, ShareRepository>();
            services.AddTransient<IPortfolioRepository, PortfolioRepository>();
            services.AddTransient<ITradeRepository, TradeRepository>();
            services.AddMvc();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("V1.1", new Info
                {
                    Version = "V1.1",
                    Title = "CrossExchange",
                    Description = "Cross Exchange trading game",
                    TermsOfService = "None"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                app.UseHttpStatusCodeExceptionMiddleware();
                app.UseExceptionHandler();
            }

            app.UseStaticFiles();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrossExchange");
            });
        }
    }
}
