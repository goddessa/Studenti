using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Models;

namespace Fakultet;
public class Startup
{
    public IConfiguration Configuration {get;}
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<FakultetContext>(options =>{
            options.UseSqlServer(Configuration.GetConnectionString("FakultetCS"));
        });
        services.AddCors(options =>{
            options.AddPolicy("CORS", builder =>{
                builder.WithOrigins(new string[]{
                    "http://localhost:8080",
                    "https://localhost:8080",
                    "http://localhost:5500",
                    "https://localhost:5500",
                    "http://127.0.0.1:8080",
                    "https://127.0.0.1:8080",
                    "http://127.0.0.1:5500",
                    "https://localhost:7277",
                    "http://localhost:5092",
                    "http://localhost:7277",
                    "https://localhost:5092"
            
                })
                .AllowAnyHeader()
                .AllowAnyMethod();

            });

        });
        services.AddControllers();
        services.AddSwaggerGen(c=>{
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Fakultet", Version = "v1"});
        });
    }
    //this method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fakultet v1"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}