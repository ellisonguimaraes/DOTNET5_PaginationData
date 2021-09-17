using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PagProj.Business;
using PagProj.Business.Interface;
using PagProj.Models.Context;
using PagProj.Repository;
using PagProj.Repository.Interface;

namespace PagProj
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
            services.AddControllers();

            ////////////////////////////////////
            // EntityFramework Inject Context //
            ////////////////////////////////////
            var connectionString = Configuration["ConnectionStrings:MySqlConnectionString"];
            services.AddDbContext<ApplicationContext> (
                op => op.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            ////////////////////////////
            // CONFIGURE Swagger Docs //
            ////////////////////////////
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Pagination Project", 
                    Version = "v1",
                    Description = "Base project for Data Pagination",
                    Contact = new OpenApiContact {
                        Name = "Ellison Guimarães",
                        Email = "ellison.guimaraes@gmail.com",
                        Url = new Uri("https://github.com/ellisonguimaraes")
                    }
                });
                
                // Configure XML Comments to Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ///////////////////////////////
            // Injection Dependency (DI) //
            ///////////////////////////////
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStudentBusiness, StudentBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PagProj v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
