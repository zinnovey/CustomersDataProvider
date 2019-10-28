using CustomersDataProvider.BusinessLogicLayer.Abstraction;
using CustomersDataProvider.BusinessLogicLayer.Services;
using CustomersDataProvider.BusinessLogicLayer.Validators;
using CustomersDataProvider.DataAccessLayer;
using CustomersDataProvider.DataAccessLayer.Abstraction;
using CustomersDataProvider.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CustomersDataProvider.WebAPI
{
    public class Startup
    {
        #region Public

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<CustomersDBContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IQueryParametersValidator, QueryParametersValidator>();
            services.AddScoped<ICustomerInfoProviderService, CustomerInfoProviderService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer information API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer information API");
            });
        }

        #endregion

    }
}
