using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using PetShop.Repository;
using Microsoft.EntityFrameworkCore;
using PetShop.Repository.Interfaces;

namespace PetShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {               
            services.AddCors();
            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddDbContext<PetDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PetshopAppCon")));
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetOwnerRepository, PetOwnerRepository>();
            services.AddScoped<IHousingRepository, HousingRepository>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PetShop Atlantico API",
                    Version = "v1",
                    Description = "Basic API for a PetShop made for Atlantico :)",
                    Contact = new OpenApiContact
                    {
                        Name = "Atlantico",
                        Email = "brenocg@alu.ufc.br",
                        Url = new Uri("https://www.atlantico.com.br/"),
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetShop Atlantico API V1");
                c.RoutePrefix = string.Empty;
            });

    
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
