using System;
using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseLibrary.API
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
      services.AddControllers(setupAction =>
      {
        setupAction.ReturnHttpNotAcceptable = true;
      }).AddXmlDataContractSerializerFormatters();

      // Input a set of assemblies. These assemblies will automatically get
      // scanned for profiles that contain mapping configurations.
      // See Profiles folder and classes inside it.
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

      services.AddDbContext<CourseLibraryContext>(options =>
      {
        options.UseNpgsql("Host=localhost;Database=jimxshaw;Username=postgres;");
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(appBuilder =>
        {
          appBuilder.Run(async context =>
          {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
          });
        });
      }

      // Marks the position in the middleware pipeline where 
      // a routing decision is made.
      app.UseRouting();

      app.UseAuthorization();

      // Marks the position in the middleware pipeline where
      // the selected endpoint is executed.
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
