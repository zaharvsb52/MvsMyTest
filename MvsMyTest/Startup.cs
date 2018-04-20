using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvsMyTest.Models;
using MvsMyTest.Services;

namespace MvsMyTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureTestingServices(services);

            ConfigureServices(services);
        }

        private void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<StuffContext>(p => p.UseInMemoryDatabase("MyDb"));
            services.AddDbContext<TagItemContext>(p => p.UseInMemoryDatabase("MyDb"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStuffService, StuffService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

    }
}
