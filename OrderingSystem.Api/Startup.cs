using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderingSystem.Api.Extensions;
using OrderingSystem.Api.Middlewares;
using OrderingSystem.Data.Context;
using OrderingSystem.Data.Repositories;
using OrderingSystem.Domain.Interfaces;

namespace OrderingSystem.Api
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
      services.AddGlobalExceptionHandlerMiddleware();

      services.AddDbContext<OrderingDataContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("Postgres"),
          action => action.MigrationsAssembly("OrderingSystem.Api")
        )
      );

      services.AddScoped<OrderingDataContext, OrderingDataContext>();
      services.AddScoped<IClientRepository, ClientRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderingSystem.Api", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderingSystem.Api v1"));
      }
      
      app.UseGlobalExceptionHandlerMiddleware();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
